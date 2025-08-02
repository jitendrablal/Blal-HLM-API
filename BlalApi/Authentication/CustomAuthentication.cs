using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json;

namespace BlalApi.Authentication
{
    public class CustomAuthenticationAttribute : ActionFilterAttribute { }
    public class CustomAuthentication : ActionFilterAttribute
    {
        private bool IsAuthenticated(HttpActionContext actionContext)
        {
            //using (DC.Data.DCContext db = new Data.DCContext())
            //using (DC.Data.DCContext db = new Data.DCContext())
            //{
            bool result = false;
            try
            {
                #region Properties

                var headers = actionContext.Request.Headers;
                var token = SecurityManager.GetHttpRequestHeader(headers, SecurityManager.HeaderAuthorizationKey);
                var userAgent = SecurityManager.GetHttpRequestHeader(headers, SecurityManager.HeaderUserAgentKey);
                var deviceId = SecurityManager.GetHttpRequestHeader(headers, SecurityManager.HeaderDeviceIdKey);

                #endregion

                #region Validate token

                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                string[] parts = key.Split(new char[] { ':' });

                if (parts.Length == 3)
                {
                    string hash = parts[0];
                    int userId = Convert.ToInt32(parts[1]);
                    long ticks = long.Parse(parts[2]);
                    DateTime timeStamp = new DateTime(ticks);

                    bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > SecurityManager.ExpirationMinutes;
                    if (!expired)
                    {
                        //var user = db.Users.FirstOrDefault(p => p.UserID == userId && p.Active);
                        //if (user != null)
                        //{
                        //string UserName = "1234";
                        string password = "Admin";
                        //string computedToken = SecurityManager.GenerateToken(userId.ToString(), password, deviceId, userAgent, ticks);
                        string computedToken = SecurityManager.GenerateToken("1234", "Admin", deviceId, userAgent, ticks);
                        result = (token == computedToken);

                        //}
                    }
                }

                #endregion

            }
            catch { }
            return result;
            //}
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            bool isAuthenticated = IsAuthenticated(actionContext);

            if (!isAuthenticated)
            {
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);

                string jsonresp = JsonConvert.SerializeObject(new { Success = false, Message = CustomError.GetError(Constants.EC_InvalidApiKey).Description, ErrorCode = Constants.EC_InvalidApiKey });
                response.Content = new StringContent(jsonresp, Encoding.UTF8, "application/json");
                
                actionContext.Response = response;
            }
        }

    }
}