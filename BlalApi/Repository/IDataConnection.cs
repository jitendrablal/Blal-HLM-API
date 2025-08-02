using BlalApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlalApi.Repository
{
    /// <summary>
    /// IDataConnection
    /// </summary>
    public interface IDataConnection
    {
        /// <summary>
        /// /ExecuteAsync
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<int> ExecuteAsync(DataConnectionParams parameter);

        /// <summary>
        /// ExecuteBoolAsync
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<bool> ExecuteBoolAsync(DataConnectionParams parameter);
        Task ExecuteScalarAsync<T>(DataConnectionParams dataConnectionParams);

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync(DataConnectionParams parameter);

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> QueryAsync<T>(DataConnectionParams parameter);

        /// <summary>
        /// QueryFirstOrDefaultAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameter"></param>
        /// <returns></returns>
        //Task<T> QueryFirstOrDefaultAsync<T>(DataConnectionParams parameter);

    }
}