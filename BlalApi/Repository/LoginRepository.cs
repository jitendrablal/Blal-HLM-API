using BlalApi.BusinessModels;
using BlalApi.Common;
using BlalApi.Controllers;
using BlalApi.Enums;
using BlalApi.Models;
using BlalApi.ViewModel;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BlalApi.Repository
{
    public class LoginRepository : BaseController
    {
        public async Task<LoginVM> GetUserbyUsernamePassword(string Username, string Password)
        {
            var dataConnectionParams = new DataConnectionParams(DataConnectionType.Text) { Query = UserLogin.userlogin(Username, Password) };
            LoginVM Model = (await dataConnection.QueryAsync<LoginVM>(dataConnectionParams)).FirstOrDefault();
            if (Model != null)
            {
                Model.Status = 1;
            }
            else
            {
                Model.Status = -1;
            }
            return Model;
        }

        public async Task<int> GetUserbyUsername(string Username)
        {
            LoginRepository repo = new LoginRepository();
            var dataConnectionParams = new DataConnectionParams(DataConnectionType.Text) { Query = UserLogin.userbyUserName(Username) };
            LoginVM Model = (await dataConnection.QueryAsync<LoginVM>(dataConnectionParams)).FirstOrDefault();
            if (Model != null)
            {
                Model.Status = 1;
            }
            else
            {
                Model.Status = 0;
            }
            return Model.Status;
        }

        public AuthModel UserAuthentication(UserLoginReqModel user)
        {
            if (user == null)
                return new AuthModel { IsAuthenticated = false, Message = "Invalid credentials." };

            string query = "SELECT * FROM api_users WHERE username = '" + user.UserName + "';";
            DataTable dtUser = StockReports.GetDataTable(query);

            if (dtUser.Rows.Count == 0)
                return new AuthModel { IsAuthenticated = false, Message = "Invalid credentials." };

            string hashedPassword = dtUser.Rows[0]["PasswordHash"].ToString();
            bool isPasswordValid = PasswordHasher.Verify(user.Password, hashedPassword);

            if (!isPasswordValid)
                return new AuthModel { IsAuthenticated = false, Message = "Invalid credentials." };

            string token = JwtManager.GenerateToken(
                dtUser.Rows[0]["id"].ToString(),
                dtUser.Rows[0]["UserName"].ToString(),
                dtUser.Rows[0]["Role"].ToString()
            );
            return new AuthModel
            {
                IsAuthenticated = true,
                UserId = dtUser.Rows[0]["UserName"].ToString(),
                AccessToken = token
            };
        }

        public UserRegistrationResult RegisterUser(RegisterUserRequest user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.UserName) || string.IsNullOrWhiteSpace(user.Password))
            {
                return new UserRegistrationResult
                {
                    IsSuccessful = false,
                    Message = "Username and password are required."
                };
            }

            try
            {
                // Check for existing user
                string checkQuery = "SELECT COUNT(*) FROM api_users WHERE Username = '" + user.UserName + "'";
                int count = Convert.ToInt32(StockReports.ExecuteScalar(checkQuery));
                if (count > 0)
                {
                    return new UserRegistrationResult
                    {
                        IsSuccessful = false,
                        Message = "Username already exists."
                    };
                }

                // Hash the password
                string passwordHash = PasswordHasher.Hash(user.Password);

                // Insert the new user
                string insertQuery = @"INSERT INTO api_users (Username,Password, PasswordHash, Role, Permissions)
                               VALUES ('" + user.UserName + "', '" + user.Password + "','" + passwordHash + "','User','Read,Write')";

                bool rowsAffected = StockReports.ExecuteDML(insertQuery);

                if (rowsAffected)
                {
                    return new UserRegistrationResult
                    {
                        IsSuccessful = true,
                        Message = "User registered successfully."
                    };
                }
                else
                {
                    return new UserRegistrationResult
                    {
                        IsSuccessful = false,
                        Message = "User registration failed."
                    };
                }
            }
            catch (Exception ex)
            {
                return new UserRegistrationResult
                {
                    IsSuccessful = false,
                    Message = "Error: " + ex.Message
                };
            }
        }
    }
}

public class UserRegistrationResult
{
    public bool IsSuccessful { get; set; }
    public string Message { get; set; }
}