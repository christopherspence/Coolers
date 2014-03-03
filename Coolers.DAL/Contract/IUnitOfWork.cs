using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.DAL.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commit all changes made in a container.
        /// </summary>
        void CommitAndRefreshChanges();

        /// <summary>
        /// Rollback any changes made
        /// </summary>
        void RollbackChanges();
    }
}
