using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlalApi.Models
{
    public class SessionPermissionModel
    {

        public string[] Can_Add { get; set; }

        public string[] Can_Edit { get; set; }

        public string[] Can_Delete { get; set; }

    }
}