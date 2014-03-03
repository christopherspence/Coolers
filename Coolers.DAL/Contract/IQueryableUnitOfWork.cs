using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.DAL.Contract
{
    public interface IQueryableUnitOfWork : IUnitOfWork, ISql
    {
        /// <summary>
        /// Returns an IDbSet instance for access to entities of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        DbSet<T> CreateSet<T>() where T : class;

        /// <summary>
        /// Attach item of type T to the ObjectStateManager
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        void Atttach<T>(T item) where T : class;

        /// <summary>
        /// Set an object of type T as "modified"
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        void SetModified<T>(T item) where T : class;

        /// <summary>
        /// Apply current values to original
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="current"></param>
        void ApplyCurrentValues<T>(T original, T current) where T : class;

    }
}
