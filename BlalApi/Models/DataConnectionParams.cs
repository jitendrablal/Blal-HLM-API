using BlalApi.Enums;
using System.Data;  

namespace BlalApi.Models
{
    /// <summary>
    /// DataConnectionParams
    /// </summary>
    public class DataConnectionParams
    {
        /// <summary>
        /// DataConnectionParams
        /// </summary>
        /// <param name="queryType"></param>
        public DataConnectionParams(DataConnectionType queryType = DataConnectionType.Text)
        {
            QueryType = queryType == DataConnectionType.Text
                ? CommandType.Text
                : CommandType.StoredProcedure;
        }

        /// <summary>
        /// Params
        /// </summary>
        public object Params { get; set; }

        /// <summary>
        /// Query
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// QueryType
        /// </summary>
        public CommandType QueryType { get; private set; }
    }
}