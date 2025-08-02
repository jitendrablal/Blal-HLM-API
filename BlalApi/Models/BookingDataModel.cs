using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlalApi.Models
{
    public class BookingDataModel
    {
        public string Id { get; set; }
        public int InterfaceCompanyID { get; set; }
        public string InterfaceCompanyName { get; set; }
        public string ReceiptNo { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string IpdOpd { get; set; }
        public string CaseNo { get; set; }
        public string Patient_ID { get; set; }
        public string PatientName { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
        public string Ward { get; set; }
        public string BarcodeNo { get; set; }
        public int CentreID { get; set; }
        public int Panel_ID { get; set; }
        public string HlmTestCode { get; set; }
        public string HlmTestName { get; set; }
        public string IsPackage { get; set; }
        public string PatTypeDesc { get; set; }
        public string PatTypeCd { get; set; }
        public string WardShortNm { get; set; }
        public string PaymentType { get; set; }
        public string GrossAmt { get; set; }
        public string DiscountAmt { get; set; }
        public string NetAmount { get; set; }
    }
}