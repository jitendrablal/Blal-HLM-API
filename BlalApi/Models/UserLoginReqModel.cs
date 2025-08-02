using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlalApi.Models
{
    public class UserLoginReqModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}