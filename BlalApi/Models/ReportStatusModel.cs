using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlalApi.Models
{
    public class ReportStatusModel
    {
        public string Test_ID { get; set; }
        public string IsSampleCollected { get; set; }
        public string BatchReceivingDate { get; set; }
        public string Approved { get; set; }
        public string ItemName { get; set; }
        
    }
    public class TestStatusModel
    {
        public string Test_ID { get; set; }
        public string Status { get; set; }
        public string ItemName { get; set; }
    }
}