using BlalApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BlalApi.Repository
{
    public class HLMSRepository
    {
        public async Task<OrderResponseDataNew> ReceiveRequest(List<BookingDataModel> reqModel)
        {
            var res = new OrderResponseDataNew { success = "false" };
            var idList = new List<string>();

            if (reqModel == null || reqModel.Count == 0)
            {
                res.message = "Data should not be empty";
                return res;
            }

            try
            {
                foreach (var item in reqModel)
                {
                    // 1. Check if the test is a package
                    string packageCheckQuery = $@"
                SELECT COUNT(1)
                FROM f_itemmaster im
                INNER JOIN f_itemmaster_interface_new imc1 ON im.ItemID = imc1.ItemID
                WHERE im.SubcategoryID = '44' AND imc1.ItemID_interface = '{item.HlmTestCode}'";

                    int isPackage = Convert.ToInt32(StockReports.ExecuteScalar(packageCheckQuery)) > 0 ? 1 : 0;

                    // 2. Calculate TotalAge
                    string totalAge = string.IsNullOrWhiteSpace(item.DOB?.ToString())
                        ? string.Empty
                        : FormatAge(Util.GetDateTime(item.DOB), DateTime.Now);

                    string registrationDate = Util.GetDateTime(item.RegistrationDate).ToString("yyyy-MM-dd HH:mm:ss");

                    // 3. If ID exists → check for update
                    if (!string.IsNullOrEmpty(item.Id))
                    {
                        var checkSql = $"SELECT * FROM booking_data WHERE ID = '{item.Id}'";
                        var dt = StockReports.GetDataTable(checkSql);

                        if (dt.Rows.Count > 0 && string.IsNullOrEmpty(dt.Rows[0]["LedgerTransactionNo"].ToString()))
                        {
                            // Perform Update
                            string updateQuery = $@"
                            UPDATE booking_data SET
                            InterfaceCompanyID = '{item.InterfaceCompanyID}',
                            InterfaceCompany = '{item.InterfaceCompanyName}',
                            WorkOrderID = '{item.ReceiptNo}',
                            RegistrationDate = '{registrationDate}',
                            PatientType = '{item.IpdOpd}',
                            CaseNo = '{item.CaseNo}',
                            Patient_ID = '{item.Patient_ID}',
                            PatientName = '{item.PatientName}',
                            Age = '{totalAge}',
                            Gender = '{item.Gender.ToUpper()}',
                            DOB = '{item.DOB}',
                            DoctorCode = '{item.DoctorCode}',
                            DoctorName = '{item.DoctorName}',
                            Ward = '{item.Ward}',
                            CentreID = '{item.CentreID}',
                            Panel_ID = '{item.Panel_ID}',
                            TestID = '{item.HlmTestCode}',
                            TestName = '{item.HlmTestName}',
                            IsPackage = '{isPackage}',
                            PatTypeDesc = '{item.PatTypeDesc}',
                            PatTypeCd = '{item.PatTypeCd}',
                            WardShortNm = '{item.WardShortNm}',
                            PaymentType = '{item.PaymentType}',
                            GrossAmt = '{item.GrossAmt}',
                            DiscountAmt = '{item.DiscountAmt}',
                            BillAmtBeforePostBillConc = '{item.NetAmount}'
                        WHERE Id = '{item.Id}';";

                            StockReports.ExecuteDML(updateQuery);
                            idList.Add($"Updated ID: {item.Id}|{item.HlmTestCode}");
                        }
                        else
                        {
                            idList.Add($"Record ID: {item.Id}|{item.HlmTestCode}");
                            res.message = "Data cannot update, because it is already processed in LIMS.";
                            res.data = idList;
                            return res;
                        }
                    }
                    else
                    {
                        // 4. Insert logic
                        string insertQuery = $@"
                    INSERT INTO booking_data (
                        InterfaceCompanyID, InterfaceCompany, WorkOrderID, RegistrationDate, PatientType,
                        CaseNo, Patient_ID, PatientName, Age, Gender, DOB, DoctorCode,
                        DoctorName, Ward, BarcodeNo, CentreID, Panel_ID, TestID, TestName,
                        IsPackage, PatTypeDesc, PatTypeCd, WardShortNm, PaymentType,
                        GrossAmt, DiscountAmt, BillAmtBeforePostBillConc)
                    VALUES (
                        '{item.InterfaceCompanyID}', '{item.InterfaceCompanyName}', '{item.ReceiptNo}', '{registrationDate}', '{item.IpdOpd}',
                        '{item.CaseNo}', '{item.Patient_ID}', '{item.PatientName}', '{totalAge}', '{item.Gender.ToUpper()}', '{item.DOB}', '{item.DoctorCode}',
                        '{item.DoctorName}', '{item.Ward}', '', '{item.CentreID}', '{item.Panel_ID}', '{item.HlmTestCode}', '{item.HlmTestName}',
                        '{isPackage}', '{item.PatTypeDesc}', '{item.PatTypeCd}', '{item.WardShortNm}', '{item.PaymentType}',
                        '{item.GrossAmt}', '{item.DiscountAmt}', '{item.NetAmount}'
                    );
                    SELECT LAST_INSERT_ID();";

                        object insertedId = StockReports.ExecuteScalar(insertQuery);
                        idList.Add($"Inserted Record: {insertedId}|{item.HlmTestCode}");
                    }
                }

                res.success = "true";
                res.message = "Data processed successfully";
                res.data = idList;
                return res;
            }
            catch (Exception ex)
            {
                new ClassLog().errLog(ex);
                res.success = "false";
                res.message = ex.Message;
                res.data = null;
                return res;
            }
        }
        public async Task<string> RejectBooking(string bookingId)
        {
            try
            {
                if (string.IsNullOrEmpty(bookingId))
                    return "Invalid booking ID";

                // 1. Check if booking exists and LedgerTransactionNo is empty
                string checkSql = $"SELECT * FROM booking_data WHERE ID = '{bookingId}'";
                DataTable dt = StockReports.GetDataTable(checkSql);

                if (dt.Rows.Count == 0)
                    return "Booking not found";

                if (!string.IsNullOrEmpty(dt.Rows[0]["LedgerTransactionNo"].ToString()))
                    return "Booking already processed in LIMS and cannot be rejected";

                string updateSql = $@"
            UPDATE booking_data 
            SET 
                STATUS = 'Reject', 
                dtReject = NOW(), 
                RejectedByID = 'HLM User', 
                RejectedBy = 'HLM User' 
            WHERE ID = '{bookingId}'";

                bool result = StockReports.ExecuteDML(updateSql);

                return result == true ? "Booking rejected successfully" : "Booking rejection failed";
            }
            catch (Exception ex)
            {
                new ClassLog().errLog(ex);
                return "Error while rejecting booking: " + ex.Message;
            }
        }
        public async Task<List<TestStatusModel>> GetReportStatus(string LedgertransactionNo)
        {
            string qry = "SELECT Test_ID,IsSampleCollected,SampleReceiveDate BatchReceivingDate,Approved,ItemName FROM patient_labinvestigation_opd WHERE LedgerTransactionNo = '" + LedgertransactionNo + "'";
            DataTable dt = StockReports.GetDataTable(qry);
            List<ReportStatusModel> data = Util.ConvertDataTableToList<ReportStatusModel>(dt);
            List<TestStatusModel> testStatusListModel = new List<TestStatusModel>();
            TestStatusModel testStatusModel = new TestStatusModel();
            foreach (var item in data)
            {
                testStatusModel = new TestStatusModel();
                if ((item.IsSampleCollected == "N" || item.IsSampleCollected == "S") && item.BatchReceivingDate == null)
                {
                    testStatusModel.Test_ID = item.Test_ID;
                    testStatusModel.Status = "Registered";
                }
                else if ((item.IsSampleCollected == "N" || item.IsSampleCollected == "S" || item.IsSampleCollected == "Y") && item.BatchReceivingDate != null && item.Approved == "0")
                {
                    testStatusModel.Test_ID = item.Test_ID;
                    testStatusModel.Status = "Batch Received";
                }
                else if ((item.IsSampleCollected == "N" || item.IsSampleCollected == "S") && item.BatchReceivingDate != null && item.Approved == "1")
                {
                    testStatusModel.Test_ID = item.Test_ID;
                    testStatusModel.Status = "Approved";
                }
                else
                {
                    testStatusModel.Test_ID = item.Test_ID;
                    testStatusModel.Status = "Approved";
                }
                testStatusModel.ItemName = item.ItemName;
                testStatusListModel.Add(testStatusModel);
            }
            return testStatusListModel;
        }
        public string GetLabNoFromBookingId(string bookingId)
        {
            try
            {
                if (string.IsNullOrEmpty(bookingId))
                    return "Invalid Booking ID";

                string sql = $"SELECT LedgerTransactionNo FROM booking_data WHERE ID = '{bookingId}'";
                DataTable dt = StockReports.GetDataTable(sql);

                if (dt.Rows.Count == 0)
                    return "Booking not found";

                string labNo = Util.GetString(dt.Rows[0]["LedgerTransactionNo"]);

                return string.IsNullOrEmpty(labNo) ? "Lab No not generated yet" : labNo;
            }
            catch (Exception ex)
            {
                new ClassLog().errLog(ex);
                return "Error while getting Lab No: " + ex.Message;
            }
        }
        public string GetReport(string labNo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(labNo))
                    return "Invalid Lab No";

                // Step 1: Check if booking exists
                string checkSql = $"SELECT LedgerTransactionNo FROM booking_data WHERE LedgerTransactionNo = '{labNo}'";
                DataTable dtBooking = StockReports.GetDataTable(checkSql);

                if (dtBooking.Rows.Count == 0)
                    return "Booking not found";

                // Step 2: Get Test IDs for that Lab No
                string testIdSql = $"SELECT GROUP_CONCAT(Test_ID) AS TestId FROM patient_labinvestigation_opd WHERE LedgerTransactionNo = '{labNo}'";
                DataTable dtTests = StockReports.GetDataTable(testIdSql);

                string testIds = dtTests.Rows.Count > 0 ? Util.GetString(dtTests.Rows[0]["TestId"]) : string.Empty;

                if (string.IsNullOrEmpty(testIds))
                    return "No Test ID(s) found for this Lab No";

                // Step 3: Build report URL
                string pdfUrl = $"https://lims6.blallab.com/Blal/Design/Lab/labreportnew.aspx?IsPrev=0&PHead=1&testid={testIds}";
                return pdfUrl;
            }
            catch (Exception ex)
            {
                new ClassLog().errLog(ex);
                return "Error while getting report: " + ex.Message;
            }
        }
        private string FormatAge(DateTime start, DateTime end)
        {

            // Compute the difference between start 

            //year and end year. 

            int years = end.Year - start.Year;

            int months = 0;

            int days = 0;

            // Check if the last year was a full year. 

            if (end < start.AddYears(years) && years != 0)
            {

                --years;

            }

            start = start.AddYears(years);

            // Now we know start <= end and the diff between them

            // is < 1 year. 

            if (start.Year == end.Year)
            {

                months = end.Month - start.Month;

            }

            else
            {

                months = (12 - start.Month) + end.Month;

            }

            // Check if the last month was a full month.

            if (end < start.AddMonths(months) && months != 0)
            {

                --months;

            }

            start = start.AddMonths(months);

            // Now we know that start < end and is within 1 month

            // of each other. 

            days = (end - start).Days;

            string Age = "";
            if (years > 0)
            {
                if (months > 6)
                {
                    years = years + 1;
                }
                Age = years.ToString() + " YRS";
            }
            else if (months > 0)
            {
                //Age = "0." + months.ToString() + " MONTH(S)";
                Age = months.ToString() + " MONTH(S)";
            }
            else
            {
                Age = days.ToString() + " DAYS(S)";
            }

            return Age;

        }
    }
}
public class OrderResponseDataNew
{
    public string success { get; set; }
    public string message { get; set; }
    public List<string> data { get; set; }
}
public class OrderResponseDataNew1
{
    public string success { get; set; }
    public string message { get; set; }
    public byte[] data { get; set; }
}
class HLMReceiver
{
    public string Base64 { get; set; }
}