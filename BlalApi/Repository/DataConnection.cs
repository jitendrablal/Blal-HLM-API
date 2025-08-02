using Dapper;
using MySql.Data.MySqlClient;
using BlalApi.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace BlalApi.Repository
{
    /// <summary>
    /// DataConnection
    /// </summary>
    public sealed class DataConnection : IDataConnection
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["LISDB"].ConnectionString;
        
        private readonly MySqlConnection connection;

        /// <summary>
        /// DataConnection
        /// </summary>
        public DataConnection()
        {
            connection = connection ?? new MySqlConnection(connectionString);
            
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<int> ExecuteAsync(DataConnectionParams parameter)
        {
            var executeAsync = connection.ExecuteAsync(parameter.Query, parameter.Params, commandType: parameter.QueryType);
            return await PerformOperationAsync(executeAsync);
        }

        /// <summary>
        /// ExecuteBoolAsync
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<bool> ExecuteBoolAsync(DataConnectionParams parameter)
        {
            var executeResult = await ExecuteAsync(parameter);
            return executeResult > 0;
        }

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<object> ExecuteScalarAsync(DataConnectionParams parameter)
        {
            var executeScalarAsync = connection.ExecuteScalarAsync(parameter.Query, parameter.Params, commandType: parameter.QueryType);
            return await PerformOperationAsync(executeScalarAsync);
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> QueryAsync<T>(DataConnectionParams parameter)
        {
            var queryAsync = connection.QueryAsync<T>(parameter.Query, parameter.Params, commandType: parameter.QueryType);
            return await PerformOperationAsync(queryAsync);
        }

        /// <summary>
        /// QueryFirstOrDefaultAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        //public async Task<T> QueryFirstOrDefaultAsync<T>(DataConnectionParams parameter)
        //{
        //    var queryFirstOrDefaultAsync = connection.QueryFirstOrDefaultAsync<T>(parameter.Query, parameter.Params, commandType: parameter.QueryType);
        //    return await PerformOperationAsync(queryFirstOrDefaultAsync);
        //}

        private async Task<T> PerformOperationAsync<T>(Task<T> task)
        {
            await connection.OpenAsync();
            var result = await task;
            await connection.CloseAsync();
            return result;
        }

        public Task ExecuteScalarAsync<T>(DataConnectionParams parameter)
        {
            var executeScalarAsync = connection.ExecuteScalarAsync(parameter.Query, parameter.Params, commandType: parameter.QueryType);
            return PerformOperationAsync(executeScalarAsync);
            //throw new System.NotImplementedException();
        }

       
    }
}