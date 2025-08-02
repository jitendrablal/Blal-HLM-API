using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Net;
using System.ComponentModel;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;


/// <summary>
/// Summary description for ClassLog
/// </summary>
public class ClassLog
{
	public ClassLog()
	{
	}
    public void GeneralLog(string exp)
    {
        FileStream fs;
        StreamWriter sw;
        if (System.IO.File.Exists("GeneralLog.txt"))
        {
            sw = File.AppendText("GeneralLog.txt");
            sw.WriteLine("  **********************************************  ");
            sw.WriteLine(" Date Time                : {0}", System.DateTime.Now.ToString());
            sw.WriteLine(" Message                  : {0}", exp);
            System.Net.IPHostEntry ip = Dns.GetHostByName(System.Net.Dns.GetHostName());
            IPAddress[] addr = ip.AddressList;
            sw.WriteLine(" Message On Machine       : {0}", System.Net.Dns.GetHostName());
            sw.WriteLine(" Machine IP Address       : {0}", addr.GetValue(0).ToString());
            sw.Close();
        }
        else
        {
            fs = File.Create("GeneralLog.txt");
            sw = new StreamWriter(fs);
            sw.WriteLine("  **********************************************  ");
            sw.WriteLine(" Date Time                : {0}", System.DateTime.Now.ToString());
            sw.WriteLine(" Message                  : {0}", exp);
            System.Net.IPHostEntry ip = Dns.GetHostByName(System.Net.Dns.GetHostName());
            IPAddress[] addr = ip.AddressList;
            sw.WriteLine(" Message On Machine       : {0}", System.Net.Dns.GetHostName());
            sw.WriteLine(" Machine IP Address       : {0}", addr.GetValue(0).ToString());
            sw.Close();
            fs.Close();
        }
    }
    public void errLog(Exception Ex)
    {
        FileStream fs;
        StreamWriter sw;
        string str = AppDomain.CurrentDomain.BaseDirectory +"ErrorLog\\"+System.DateTime.Now.ToString("dd-MMM-yyyy")+"\\";
        if (System.IO.File.Exists(str + "ErrorLog.txt"))
        {
            sw = File.AppendText(str + "ErrorLog.txt");
            sw.WriteLine("\n");
            sw.WriteLine("  **********************************************************  ");
            sw.WriteLine(" Time Of Error            : {0}", System.DateTime.Now.ToString());
            sw.WriteLine(" Error Message            : {0}", Ex.Message);
            sw.WriteLine(" Error Exeption            : {0}", Ex.InnerException);
            sw.WriteLine(" Error Place              : {0}", Ex.StackTrace.ToString());
            System.Net.IPHostEntry ip = Dns.GetHostByName(System.Net.Dns.GetHostName());
            IPAddress[] addr = ip.AddressList;
            sw.WriteLine(" Error On Machine         : {0}", System.Net.Dns.GetHostName());
            sw.WriteLine(" Error Machine IP Address : {0}", addr.GetValue(0).ToString());
            sw.Close();
        }
        else
        {
            System.IO.Directory.CreateDirectory(str);
            fs = File.Create(str + "ErrorLog.txt");
            sw = new StreamWriter(fs);
            sw.WriteLine("  **********************************************************  ");
            sw.WriteLine(" Time Of Error            : {0}", System.DateTime.Now.ToString());
            sw.WriteLine(" Error Message            : {0}", Ex.Message);
            sw.WriteLine(" Error Exeption            : {0}", Ex.InnerException);
            sw.WriteLine(" Error Place              : {0}", Ex.StackTrace.ToString());
            System.Net.IPHostEntry ip = Dns.GetHostByName(System.Net.Dns.GetHostName());
            IPAddress[] addr = ip.AddressList;
            sw.WriteLine(" Error On Machine         : {0}", System.Net.Dns.GetHostName());
            sw.WriteLine(" Error Machine IP Address : {0}", addr.GetValue(0).ToString());
            sw.Close();
            fs.Close();
        }
    }

    public void GenLog(string Ex)
    {
        FileStream fs;
        StreamWriter sw;
        string str = AppDomain.CurrentDomain.BaseDirectory + "ErrorLog\\" + System.DateTime.Now.ToString("dd-MMM-yyyy") + "\\";
        if (System.IO.File.Exists(str + "ErrorLog.txt"))
        {
            sw = File.AppendText(str + "ErrorLog.txt");
            sw.WriteLine("\n");
            sw.WriteLine("  **********************************************************  ");
            sw.WriteLine(" Time Of Error            : {0}", System.DateTime.Now.ToString());
            sw.WriteLine(" Error Message            : {0}", Ex);
            System.Net.IPHostEntry ip = Dns.GetHostByName(System.Net.Dns.GetHostName());
            IPAddress[] addr = ip.AddressList;
            sw.WriteLine(" Error On Machine         : {0}", System.Net.Dns.GetHostName());
            sw.WriteLine(" Error Machine IP Address : {0}", addr.GetValue(0).ToString());
            sw.Close();
        }
        else
        {
            System.IO.Directory.CreateDirectory(str);
            fs = File.Create(str + "ErrorLog.txt");
            sw = new StreamWriter(fs);
            sw.WriteLine("  **********************************************************  ");
            sw.WriteLine(" Time Of Error            : {0}", System.DateTime.Now.ToString());
            sw.WriteLine(" Error Message            : {0}", Ex);
            System.Net.IPHostEntry ip = Dns.GetHostByName(System.Net.Dns.GetHostName());
            IPAddress[] addr = ip.AddressList;
            sw.WriteLine(" Error On Machine         : {0}", System.Net.Dns.GetHostName());
            sw.WriteLine(" Error Machine IP Address : {0}", addr.GetValue(0).ToString());
            sw.Close();
            fs.Close();
        }
    }
}
