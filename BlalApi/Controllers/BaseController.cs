using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlalApi.Authentication;
using BlalApi.Enums;
using BlalApi.Models;
using BlalApi.Repository;

namespace BlalApi.Controllers
{
   // [AllowCrossSiteJson]
    public class BaseController : Controller
    {
        // GET: Base
        #region GlobalProperties
        public IDataConnection dataConnection;
        public IDataConnection dataConnectionLIS6;
        #endregion

        #region ctor
        public BaseController()
        {
            dataConnection = dataConnection ?? new DataConnection();
            dataConnectionLIS6 = dataConnectionLIS6 ?? new DataConnection();
        }
        #endregion

        public static string Encrypt(string password)
        {
            try
            {
                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        } //this function Convert to Decord your Password

        public string Decrypt(string encodedData)
        {
            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encodedData);
            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            string result = new String(decoded_char);
            return result;
        }

        public async Task<int> UpdateStatus(int Id, string ProcedureName)
        {
            var dataConnectionParams = new DataConnectionParams(DataConnectionType.StoredProcedure)
            {
                Query = ProcedureName,
                Params = new
                {
                    Id = Id
                }
            };
            int result = await dataConnection.ExecuteAsync(dataConnectionParams);
            return result;
        }
    }
}