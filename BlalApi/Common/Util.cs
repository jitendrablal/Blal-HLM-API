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
using System.Xml;
using System.Text;
using cfg = System.Configuration.ConfigurationManager;
using System.IO;
using System.Diagnostics;
using System.Web.SessionState;
using System.Collections.Generic;
using System.Globalization;


/// <summary>
/// <CreatedBy>Jitendra Singh</CreatedBy>
/// <CreatedOn>20st Aug, 2021</CreatedOn>
/// <ModifiedBy></ModifiedBy>
/// Summary description for Util
/// </summary>
public sealed class Util
{
    public Util() { }

    public static string GetConString()
    {

        return ConfigurationManager.ConnectionStrings["LISDB"].ConnectionString;
    }

    public static string GetStringWithoutReplace(Object obj)
    {
        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return "";
        else
        {
            return obj.ToString();
        }
    }
    public static string SiteId()
    {
        return cfg.AppSettings["SiteId"];
    }

    public static MySqlConnection GetMySqlCon()
    {
        MySqlConnection objCon = new MySqlConnection(GetConString());
        return objCon;
    }
    public static string getApp(string KeyID)
    {
        return cfg.AppSettings[KeyID];
    }
    public static string getHash()
    {
        string hash = Util.GetString(System.Guid.NewGuid()) + "" + DateTime.Now.ToString("MMddyyyyHHmmss");
        return hash;
    }
    public static string GetParamaterString(params string[] strParam)
    {
        #region GetParamaterString
        try
        {
            string strParamValue = "";

            for (int i = strParam.GetLowerBound(0); i <= strParam.GetUpperBound(0); i++)
            {
                if (i % 2 == 0)
                {
                    strParamValue = strParamValue + strParam.GetValue(i).ToString() + "=";
                }
                else
                {
                    strParamValue = strParamValue + strParam.GetValue(i).ToString() + ",";
                }
            }

            if (strParamValue.Length > 0)
            {
                strParamValue = strParamValue.Substring(0, strParamValue.Length - 1);
            }
            return strParamValue;
        }
        catch
        {
            return "";
        }
        #endregion
    }

    #region Handle Is DbNull
    public static int GetInt(Object obj)
    {
        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return 0;
        else
            return int.Parse(obj.ToString());
    }
    public static Int16 GetShortInt(Object obj)
    {
        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return 0;
        else
            return Int16.Parse(obj.ToString());


    }
    public static long GetLong(Object obj)
    {
        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return 0;
        else
            return long.Parse(obj.ToString());

    }

    public static decimal GetDecimal(Object obj)
    {

        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return 0;
        else
            return Decimal.Parse(obj.ToString());
    }

    public static float GetFloat(Object obj)
    {

        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return 0F;
        else
            return float.Parse(obj.ToString());
    }
    public static double GetDouble(Object obj)
    {

        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return 0;
        else
            return double.Parse(obj.ToString());
    }
    public static DateTime GetDateTime(object obj)
    {
        try
        {
            if (obj == null || Convert.IsDBNull(obj))
                return Util.GetMinDateTime();

            string dateStr = obj.ToString().Trim();

            if (string.IsNullOrEmpty(dateStr) || dateStr.StartsWith("0000"))
                return Util.GetMinDateTime();

            string[] formats = {
            "MM/dd/yyyy",
            "yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss",
            "dd/MM/yyyy",
            "dd-MM-yyyy",
            "dd-MM-yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm:ss"
        };

            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                return result;

            if (DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                return result;

            return Util.GetMinDateTime();
        }
        catch (Exception ex)
        {
            // Optional: log exception if needed
            return Util.GetMinDateTime();
        }
    }

    public static string GetString(Object obj)
    {

        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return "";
        else
            return obj.ToString();
    }

    public static bool GetBoolean(Object obj)
    {
        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return false;
        else
            return bool.Parse(obj.ToString());
    }
    #endregion

    public static DateTime GetMinDateTime()
    {
        return DateTime.Parse("01/jan/0001");
    }

    public static int TimeDiffInMin(DateTime StartTime, DateTime EndTime)
    {

        TimeSpan ts = EndTime.Subtract(StartTime);
        int hours = ts.Hours;
        int TotalMin = (hours * 60) + ts.Minutes;
        return TotalMin;

    }

    public static string GetCentreAccessList(string CentreId)
    {
        return "select distinct cm.CentreID,cm.Centre from centre_master cm where ( cm.CentreID in (select CentreAccess from centre_access where CentreID ='" + CentreId + "' and AccessType=2 ) or cm.CentreID = '" + CentreId + "') and cm.isActive=1 order by cm.Centre ";
    }
    public static string GetCentrePanelAccessList(string CentreId)
    {
        return " SELECT pm.Panel_ID,pm.Company_Name FROM  f_panel_master pm WHERE pm.CentreGroupID=(SELECT CentreGroupID FROM centre_master WHERE CentreID='" + CentreId + "')AND pm.IsActive=1  ORDER BY pm.Company_Name ";
    }

    public static string GetCentreGroup()
    {
        return "SELECT ID ,NAME  FROM f_centre_group WHERE  active=1  ORDER BY id";
    }

    public static string GetDiagnosisList()
    {
        return "SELECT ID,Diagnosis FROM patient_Diagnosis WHERE IsActive = 1";
    }

    public static string GetOutSourceList()
    {

        return "SELECT Id,NAME FROM outsourcelabmaster WHERE Active=1 ORDER BY NAME ";
    }

    public static string GetReferSourceList()
    {
        return "select SourceID,Source from f_ReferSource";
    }

    public static string GetPanelGroupWise(string GroupID)
    {
        return "SELECT Company_Name,CONCAT(Panel_ID,'#',ReferenceCodeOPD)PanelID FROM f_panel_master WHERE PanelGroupID='" + GroupID + "' ";
    }

    public static string GetpatientType()
    {
        return "SELECT ID,TypeName FROM patienttype_master WHERE IsActive=1 order by ID ";
    }

    public static string GetApprovedByDoctorQuery()
    {
        return "SELECT em.Name,al.EmployeeID FROM employee_master em INNER JOIN (SELECT DISTINCT(EmployeeID) FROM f_approval_labemployee)al ON em.Employee_ID=al.EmployeeID and em.IsActive=1 order by em.NAME ";
    }

    //lalit invoice
    public static string CCpanelchanges(string id)
    {
        return "panel_id in (SELECT panelid FROM centre_panel WHERE CentreId IN (SELECT BusinessUnitID FROM f_panel_master WHERE employeeID='" + HttpContext.Current.Session["ID"].ToString() + "' )) ";
    }
    public static string GetInvoicePanelQuery()
    {
        return "SELECT ( SELECT pm2.Company_Name Company_Name  FROM f_panel_master pm2 WHERE pm2.Panel_id=pm.invoicepanelid)Company_Name ,pm.invoicepanelid Panel_ID FROM f_panel_master pm INNER JOIN `centre_panel` cp ON pm.`Panel_ID`=cp.`PanelId` INNER JOIN f_login fl ON  cp.`CentreId`=fl.`CentreID` WHERE fl.employeeID='" + HttpContext.Current.Session["ID"].ToString() + "' AND fl.RoleID='" + HttpContext.Current.Session["RoleID"].ToString() + "' AND pm.isActive=1 AND pm.`IsInvoicePanel`=1 GROUP BY `invoicepanelid` ORDER BY Company_Name ";
    }
    public static string changeNumericToWords(string numb)
    {
        string num = numb.ToString();
        return changeToWords(num, false);
    }
    public static string changeToWords(String numb, bool isCurrency)
    {
        string val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
        string endStr = (isCurrency) ? ("Only") : ("");
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (decimalPlace > 0)
            {
                wholeNo = numb.Substring(0, decimalPlace);
                points = numb.Substring(decimalPlace + 1);
                if (Convert.ToInt32(points) > 0)
                {
                    andStr = (isCurrency) ? ("and") : ("point");// just to separate whole numbers from points/cents
                    endStr = (isCurrency) ? ("Cents " + endStr) : ("");
                    pointStr = translateCents(points);
                }
            }
            val = String.Format("{0} {1}{2} {3}", translateWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
        }
        catch {; }
        return val;
    }

    private static string translateCents(String cents)
    {
        String cts = "", digit = "", engOne = "";
        for (int i = 0; i < cents.Length; i++)
        {
            digit = cents[i].ToString();
            if (digit.Equals("0"))
            {
                engOne = "Zero";
            }
            else
            {
                engOne = ones(digit);
            }
            cts += " " + engOne;
        }
        return cts;
    }
    private static string translateWholeNumber(String number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX
            bool isDone = false;//test if already translated
            double dblAmt = (Convert.ToDouble(number));
            //if ((dblAmt > 0) && number.StartsWith("0"))
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric
                beginsZero = number.StartsWith("0");

                int numDigits = number.Length;
                int pos = 0;//store digit grouping
                String place = "";//digit grouping name:hundres,thousand,etc...
                switch (numDigits)
                {
                    case 1://ones' range
                        word = ones(number);
                        isDone = true;
                        break;
                    case 2://tens' range
                        word = tens(number);
                        isDone = true;
                        break;
                    case 3://hundreds' range
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7://millions' range
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10://Billions's range
                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    //add extra case options for anything above Billion...
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)
                    word = translateWholeNumber(number.Substring(0, pos)) + place + translateWholeNumber(number.Substring(pos));
                    //check for trailing zeros
                    if (beginsZero) word = " and " + word.Trim();
                }
                //ignore digit grouping names
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch {; }
        return word.Trim();
    }
    private static string tens(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = null;
        switch (digt)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (digt > 0)
                {
                    name = tens(digit.Substring(0, 1) + "0") + " " + ones(digit.Substring(1));
                }
                break;
        }
        return name;
    }
    private static string ones(String digit)
    {
        int digt = Convert.ToInt32(digit);
        String name = "";
        switch (digt)
        {
            case 1:
                name = "One";
                break;
            case 2:
                name = "Two";
                break;
            case 3:
                name = "Three";
                break;
            case 4:
                name = "Four";
                break;
            case 5:
                name = "Five";
                break;
            case 6:
                name = "Six";
                break;
            case 7:
                name = "Seven";
                break;
            case 8:
                name = "Eight";
                break;
            case 9:
                name = "Nine";
                break;
        }
        return name;
    }
    // for crl invoice
    public static string GetPanelMasterAll()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("   SELECT DISTINCT(CONCAT(pm.Panel_code,' = ',pm.Company_Name)) Company_Name, pm.Panel_ID FROM f_panel_master pm ");
        sb.Append(" INNER JOIN centre_panel cp ON cp.PanelId=pm.Panel_Id INNER JOIN centre_master cm ON cm.CentreID=cp.CentreID ");
        sb.Append(" INNER JOIN f_login fl ON fl.CentreID=cm.CentreID WHERE pm.isActive=1 AND  fl.employeeID='" + Util.GetString(HttpContext.Current.Session["id"]) + "' ");
        sb.Append(" AND fl.RoleID='" + Util.GetString(HttpContext.Current.Session["RoleID"]) + "'  ORDER BY Company_Name ");
        return sb.ToString();
    }
    public static string FirstCharToUpper(string input)
    {
        switch (input)
        {
            case null: throw new ArgumentNullException(nameof(input));
            case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            default: return input[0].ToString().ToUpper() + input.Substring(1).ToLower();
        }
    }
    public static string GetDateFormat(string date)
    {
        DateTime delTime = Convert.ToDateTime(date);
        string dDate = delTime.ToString("dd-MMM-yyyy");
        string dTime = delTime.ToString("HH:mm:ss");
        TimeSpan timespan = new TimeSpan(Convert.ToInt16(delTime.ToString("HH")), Convert.ToInt16(delTime.ToString("mm")), Convert.ToInt16(delTime.ToString("ss")));
        DateTime time = DateTime.Today.Add(timespan);
        return Util.GetString(dDate + " " + time.ToString("hh:mm tt"));
    }
    public static string getip()
    {
        try
        {
            string myIP = "";
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                myIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
            {
                myIP = HttpContext.Current.Request.UserHostAddress;

            }
            return myIP;
        }
        catch
        {
            return string.Empty;
        }
    }
    public static Byte GetByte(Object obj)
    {
        if (obj == null || Convert.IsDBNull(obj) || obj.ToString().Trim() == string.Empty)
            return 0;
        else
            return Byte.Parse(obj.ToString());
    }
    public static string getBillNo(int CentreID, string Type, MySqlConnection con, MySqlTransaction tnx)
    {
        using (MySqlCommand cmd = new MySqlCommand("f_Generate_Bill_No", con, tnx))
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new MySqlParameter("_CentreID", CentreID));
            cmd.Parameters.Add(new MySqlParameter("_Type", Type));
            cmd.Parameters.Add("Bill_No", MySqlDbType.VarChar, 25);
            cmd.Parameters["Bill_No"].Direction = ParameterDirection.Output;
            cmd.ExecuteScalar();
            return (string)cmd.Parameters["Bill_No"].Value;
        }
    }
    public static string FirstCharToCharArray(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        var stringArray = input.ToCharArray();
        if (char.IsLower(stringArray[0]))
        {
            stringArray[0] = char.ToUpper(stringArray[0]);
        }
        return new string(stringArray);
    }
    public static List<T> ConvertDataTableToList<T>(DataTable dt) where T : new()
    {
        List<T> list = new List<T>();
        foreach (DataRow row in dt.Rows)
        {
            T obj = new T();
            foreach (DataColumn col in dt.Columns)
            {
                var prop = obj.GetType().GetProperty(col.ColumnName);
                if (prop != null && row[col] != DBNull.Value)
                    try
                    {
                        prop.SetValue(obj, row[col].ToString());
                    }
                    catch (Exception)
                    {
                        prop.SetValue(obj, Convert.ToInt32(row[col]));
                    }

            }
            list.Add(obj);
        }
        return list;
    }
    public static T ConvertDataTableToModel<T>(DataTable dt) where T : new()
    {
        T list = new T();
        DataRow row = dt.Rows[0];

        T obj = new T();
        foreach (DataColumn col in dt.Columns)
        {
            var prop = obj.GetType().GetProperty(col.ColumnName);
            if (prop != null && row[col] != DBNull.Value)
                try
                {
                    prop.SetValue(obj, row[col].ToString());
                }
                catch (Exception)
                {
                    prop.SetValue(obj, Convert.ToInt32(row[col]));
                }
        }
        return obj;
    }
    public static string Get_DateTime(object obj)
    {
        try
        {
            // Check for null or DBNull
            if (obj == null || Convert.IsDBNull(obj))
                return Util.GetString(Util.GetMinDateTime().ToString("yyyy-MM-dd hh:mm:ss"));

            string dateStr = obj.ToString().Trim();

            // Check for empty or invalid date string
            if (string.IsNullOrEmpty(dateStr) || dateStr.StartsWith("0000"))
                return Util.GetString(Util.GetMinDateTime().ToString("yyyy-MM-dd hh:mm:ss"));

            string[] formats = {
            "MM/dd/yyyy",
            "yyyy-MM-dd",
            "yyyy-MM-dd HH:mm:ss",
            "dd/MM/yyyy",
            "dd-MM-yyyy",
            "dd-MM-yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm:ss"
        };

            // Try parsing with known formats
            if (DateTime.TryParseExact(dateStr, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result) ||
                DateTime.TryParse(dateStr, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return Util.GetString(result.ToString("yyyy-MM-dd hh:mm:ss"));
            }

            // If parsing fails
            return Util.GetString(Util.GetMinDateTime().ToString("yyyy-MM-dd hh:mm:ss"));
        }
        catch (Exception ex)
        {
            // Optional: Log the exception
            // Logger.LogError(ex, "Error parsing datetime");

            return Util.GetString(Util.GetMinDateTime().ToString("yyyy-MM-dd hh:mm:ss"));
        }
    }

}