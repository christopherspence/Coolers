using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Common.Validator.Contract
{
    public interface IEntityValidator 
    {
        /// <summary>
        /// Returns true if there are no validation issues.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity</typeparam>
        /// <param name="item">the entity to validate</param>
        /// <returns></returns>
        bool IsValid<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Returns a list of errors if the entity isn't valid
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        IEnumerable<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class;
    }
}
