using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MySql.Data.MySqlClient;
using System.Text;
using cfg = System.Configuration.ConfigurationManager;
using System.Xml;
using System.IO;

/// <summary>
/// Summary description for AllGlobalFunction
/// </summary>
public class AllGlobalFunction
{
    public const string LabID = "LAB", BranchID = "BRANCH";
    public static string Location = cfg.AppSettings["Location"];
    public static string LocationIPD = cfg.AppSettings["LocationIPD"];
    public static string LocationPur = cfg.AppSettings["LocationPur"];
    public static string LocationPatReturn = cfg.AppSettings["LocationPatReturn"];
    public static string LocationUpdate = cfg.AppSettings["LocationUpdate"];
    public static string LocationNMPurchase = cfg.AppSettings["LocationNMPurchase"];
    public static string LocationAdjust = cfg.AppSettings["LocationAdjust"];
    public static string NonMedicalAdjust = cfg.AppSettings["NonMedicalAdjust"];
    public static string LabIPD = cfg.AppSettings["LabIPD"];
    public static string LabOPD = cfg.AppSettings["LabOPD"];
    public static string HospCode = cfg.AppSettings["HospCode"];
    public static string THospCode = cfg.AppSettings["THospCode"];
    public static string CHospCode = cfg.AppSettings["CHospCode"];
    public static string LHospCode = cfg.AppSettings["LHospCode"];
    public static string Panel = cfg.AppSettings["Panel"];
    public static string IsWorkstation = cfg.AppSettings["IsWorkstation"];
    public static string EmailSubject = cfg.AppSettings["MailSub"];
    public static string EmailID_From = cfg.AppSettings["FromMail"];
    public static string MedicalDeptLedgerNo = cfg.AppSettings["MedicalDeptLedgerNo"];
    public static string GeneralDeptLedgerNo = cfg.AppSettings["GeneralDeptLedgerNo"];
    public static string ShowAllInv = cfg.AppSettings["ShowAllInv"];
    public static string[] MedicineType = { "Tablet", "Syrup", "EyeDrop", "EarDrop", "NosalDrop", "Capsule", "Tube", "Powder", "Lotion", "Cream", "Immunization", "Injection", "Suspension", "Emulsion", "Sachet", "Inhaler", "Rotacap", "Puff", "Pellet", "Ointment" };
    public static string[] MedicineTab = { "", "0", "1", "2", "1/2", "3/4", "3", "4" };
    public static string[] MedicineTimes = { "", "SOS", "HS", "BD", "OD", "TDS", "1 Hourly", "2 Hourly", "3 Hourly", "6 Hourly", "12 Hourly", "State", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20" };
    public static string[] MedicineDays = { "", "0 Day", "1 Day", "2 Days", "3 Days", "4 Days", "5 Days", "6 Days", "7 Days", "1 Week ", "2 Weeks ", "3 Weeks ", "1 Month ", "2 Months ", "3 Months " };
    public static string[] SymptomDuration = { "0 Day", "1 Day", "2 Days", "3 Days", "4 Days", "5 Days", "6 Days", "7 Days", "1 Week ", "2 Weeks ", "3 Weeks ", "1 Month ", "2 Months ", "3 Months ", "4 Months ", "5 Months ", "1/2 yr ", "1 yr ", "2 yrs ", "> 2 yrs " };
    public static string[] BloobGroup = { "NA", "O+", "O-", "A+", "B+", "AB+", "A-", "B-" };
    public static string[] NameTitle = { "Mr.", "Mrs.", "Ms.", "Baby", "Baba", "Master", "Dr." };
    public static string[] Eye = { "LE", "RE" };
    public static string[] VisionType = { "Unaided", "Aided", "With Pin Hole" };
    public static string[] VisionValue = { " ", "6/60", "6/60(p)", "6/36", "6/36(p)", "6/18", "6/18(p)", "6/12", "6/12(p)", "6/9", "6/9(p)", "6/6", "6/6(p)", "6/5" };
    public static string[] PreviousGlassShape = { "Spherical", "Cylndrical" };
    public static string[] PreviousGlassType = { "Convex", "Concave" };
    public static string[] PreviousGlassValue = { " ", "0.25", "0.50", "0.75", "1.00", "1.25", "1.50", "1.75", "2.00", "2.25", "2.50", "2.75", "3.00", "3.25", "3.50", "3.75", "4.00", "4.25", "4.50", "4.75", "5.25", "5.50", "5.75", "6.00", "6.25", "6.50", "6.75", "7.00", "7.25", "7.50", "7.75", "8.00", "8.25", "8.50", "8.75", "9.00", "9.25", "9.50", "9.75", "10.25", "10.50", "10.75", "11.00", "11.25", "11.50", "11.75", "12.00", "12.25", "12.50", "12.75", "13.00", "13.25", "13.50", "13.75", "14.00", "14.25", "14.50", "14.75", "15.25", "15.50", "15.75", "16.00", "16.25", "16.50", "16.75", "17.00", "17.25", "17.50", "17.75", "18.00", "18.25", "18.50", "18.75", "19.00", "19.25", "19.50", "19.75" };
    public static string[] CylindricalAngle = { " ", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "100", "101", "102", "103", "104", "105", "106", "107", "108", "109", "110", "111", "112", "113", "114", "115", "116", "117", "118", "119", "120", "121", "122", "123", "124", "125", "126", "127", "128", "129", "130", "131", "132", "133", "134", "135", "136", "137", "138", "139", "140", "141", "142", "143", "144", "145", "146", "147", "148", "149", "150", "151", "152", "153", "154", "155", "156", "157", "158", "159", "160", "161", "162", "163", "164", "165", "166", "167", "168", "169", "170", "171", "172", "173", "174", "175", "176", "177", "178", "179", "180" };
    public static string[] KinRelation = { "Father", "Mother", "Husband", "Wife", "Son", "Daughter", "Relative", "Uncle", "Aunty", "Sister", "Brother", "Spouse", "Self" };
    public static string[] DischargeType = { "Normal", "Lama", "Absconding", "Discharge On Request", "Expired", "Patient On Leave" };
    public static string showqrcodeinprintlabreport = cfg.AppSettings["showqrcodeinprintlabreport"];
    public static string MachineDB = cfg.AppSettings["MachineDB"];
}
