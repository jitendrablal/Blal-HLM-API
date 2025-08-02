using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlalApi.Models
{
    public class Response
    {


        public int status_Code { get; set; }

        public String success_message { get; set; }

        public String error_message { get; set; }

        public Object data { get; set; }

        public static Response GetResponse(int statusCode, Object data = null, string response_message = "", string error_message = "")
        {
            return new Response { status_Code = statusCode, success_message = response_message, data = data, error_message = error_message };
        }
    }
}