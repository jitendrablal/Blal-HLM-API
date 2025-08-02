using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;
using BlalApi.Authentication;
using BlalApi.Common;
using BlalApi.Models;
using BlalApi.Repository;
using log4net;

namespace BlalApi.Controllers
{
    public class LoginController : ApiController
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LoginController));
        //[System.Web.Http.Route("~/api/login")]
        //[System.Web.Http.HttpPost]
        //public HttpResponseMessage Login(string UserName, string Password)
        //{
        //    try
        //    {
        //        HttpRequestHeaders headers = this.Request.Headers;
        //        string deviceType = string.Empty;
        //        string app_version = string.Empty;
        //        string Content_Type = string.Empty;

        //        UserLoginReqModel userDto = new UserLoginReqModel();
        //        userDto.UserName = UserName;
        //        userDto.Password = Password;
        //        string userAgent = SecurityManager.GetHttpRequestHeader(headers, SecurityManager.HeaderUserAgentKey);
        //        string deviceId = SecurityManager.GetHttpRequestHeader(headers, SecurityManager.HeaderDeviceIdKey);
        //        if (userDto.UserName == "1234" && userDto.Password == "abcd")
        //        {
        //            long timeStamp = DateTime.UtcNow.Ticks;
        //            string token = SecurityManager.GenerateToken("1234", "Admin", deviceId, userAgent, timeStamp);

        //            AuthModel auth = new AuthModel { UserId = 1234, AccessToken = token };
        //            return ApiResult.GetResult(Request, auth);
        //        }
        //        else
        //        {

        //            AuthModel auth = new AuthModel { UserId = 0, AccessToken = "", Message = "Invalid credentials" };
        //            return ApiResult.GetResult(Request, auth);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return ApiResult.GetErrorResult(Request, ex, string.Empty);
        //    }
        //}

        [System.Web.Http.Route("~/api/login")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Login(UserLoginReqModel user)
        {
            LoginRepository _loginRepository = new LoginRepository();    
            var result = _loginRepository.UserAuthentication(user);

            if (!result.IsAuthenticated)
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized, new
                {
                    message = result.Message
                });
            }

            return Request.CreateResponse(HttpStatusCode.OK, new
            {
                userId = result.UserId,
                accessToken = result.AccessToken
            });
        }
        //[System.Web.Http.Route("~/api/register")]
        //[System.Web.Http.HttpPost]
        //public HttpResponseMessage Register(RegisterUserRequest user)
        //{
        //    LoginRepository _loginRepository = new LoginRepository();
        //    var result = _loginRepository.RegisterUser(user); 

        //    if (!result.IsSuccessful)
        //    {
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, new { message = result.Message });
        //    }

        //    return Request.CreateResponse(HttpStatusCode.Created, new { message = result.Message });
        //}
    }
}
