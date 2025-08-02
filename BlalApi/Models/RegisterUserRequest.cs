using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlalApi.Models
{

    public class RegisterUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Optional: default to "User"
        public string Permissions { get; set; } // Optional: e.g. "read,write"
    }
}