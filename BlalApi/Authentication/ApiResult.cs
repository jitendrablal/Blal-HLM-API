using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace BlalApi.Authentication
{
    public static class ApiResult
    {
        public static HttpResponseMessage GetResult(HttpRequestMessage request, object obj, bool success = true, string message = "", string errorCode = "")
        {
            string jsonresp = JsonConvert.SerializeObject(new { Result = obj, Success = success, Message = message, ErrorCode = errorCode });

            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonresp, Encoding.UTF8, "application/json");
            return response;
        }

        public static HttpResponseMessage GetErrorResult(HttpRequestMessage request, Exception ex, params string[] customData)
        {
            Error customError;

            string error = string.IsNullOrEmpty(ex.Message) ? ex.GetBaseException().Message : ex.Message;

            customError = CustomError.GetError(error);
            if (customError == null)
            {
                if (error.Contains("Object reference"))
                {
                    customError = CustomError.GetError(Constants.EC_ObjectReferenceNull);
                }
                else if (error.Contains("donation has already been associated with another donor"))
                {
                    customError = CustomError.GetError(Constants.EC_ContactLessAlreadyLinkedAnotherDonor);
                }
                else
                {
                    customError = CustomError.GetError(Constants.EC_InternalServerError);
                }
            }

            object obj = null;
            string jsonresp = JsonConvert.SerializeObject(new { Result = obj, Success = false, Message = string.Format("{0}, {1}", customError.Description, string.Join(",", customData)).Trim(',').Trim(), ErrorCode = customError.Code });
            //string jsonresp = JsonConvert.SerializeObject(new { Result = obj, Success = false, Message = ex.GetBaseException().Message, ErrorCode = customError.Code });

            var response = request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(jsonresp, Encoding.UTF8, "application/json");
            return response;
        }
    }
}
