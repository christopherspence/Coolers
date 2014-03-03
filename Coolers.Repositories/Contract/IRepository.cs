using Coolers.DAL.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Repositories.Contract
{
    public interface IRepository<T> : IDisposable where T : class
    {
        /// <summary>
        /// Get the unit of work for this repo
        /// </summary>
        IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        /// Add item into repository
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Delete item
        /// </summary>
        /// <param name="item"></param>
        void Remove(T item);

        /// <summary>
        /// Set item as modified
        /// </summary>
        /// <param name="item"></param>
        void Modify(T item);

        /// <summary>
        /// Get element by key
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get(Guid id);

        /// <summary>
        /// Get all elements
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get all matching elements of type T in repo.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        IEnumerable<T> GetFiltered(Expression<Func<T, bool>> filter);

    }
}
