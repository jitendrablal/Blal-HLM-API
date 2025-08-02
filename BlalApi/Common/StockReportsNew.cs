using MySql.Data.MySqlClient;
using System.Data;
using System.Text;
using System.Web;
using System;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
/// <summary>
/// Helper Class for Stock Related Data
/// </summary>
public class StockReportsNew
{
    public async Task<DataTable> GetDataTableNew(string query)
    {
        DataTable dataTable = new DataTable();

        try
        {
            using (MySqlConnection connection = Util.GetMySqlCon())
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log or handle the exception as needed
            ClassLog objClassLog = new ClassLog();
            objClassLog.GenLog("GetDataTableNew - catch:" + ex.StackTrace);
            throw; // Rethrow the exception if you want it to bubble up
        }

        return dataTable;
    }

}