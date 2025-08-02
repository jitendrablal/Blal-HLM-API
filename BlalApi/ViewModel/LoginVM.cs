using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlalApi.ViewModel
{
    public class LoginVM
    {
        public string UserName { set; get; }
        public string Password { set; get; }
        public int Status { get; set; }
    }
}