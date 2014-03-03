using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.DAL.Contract
{
    public interface ISql
    {
        /// <summary>
        /// Execute a raw sql query against datastore
        /// </summary>
        /// <typeparam name="T">Entity type to map result to</typeparam>
        /// <param name="query">ex: select * from whatever</param>
        /// <param name="parameters">Parameters used in the query</param>
        /// <returns>The results of the query</returns>
        IEnumerable<T> ExecuteQuery<T>(string query, params object[] parameters);

        /// <summary>
        /// Execute a command against datastore
        /// </summary>
        /// <param name="sqlCommand">insert into someTable (,,,) values (,,,) </param>
        /// <param name="parameters">Parameters used in query</param>
        /// <returns>Number of affected records</returns>
        int ExecuteCommand(string sqlCommand, params object[] parameters);
    }
}
