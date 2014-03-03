using Coolers.DAL.Contract;
using Coolers.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Repositories.Implementation
{
   public class Repository<T> : IRepository<T> where T : class
    {
        IQueryableUnitOfWork _unitOfWork;

        public IQueryableUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        public Repository(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == (IUnitOfWork)null)
                throw new ArgumentNullException("unitOfWork");

            _unitOfWork = unitOfWork;
        }

        #region IRepository members

        public virtual void Add(T item)
        {
            if (item != (T)null)
                GetSet().Add(item);
            else
                throw new NullReferenceException("Cannot add a null entity");
        }

        public virtual void Remove(T item)
        {
            if (item != (T)null)
            {
                // in case it doesn't exist
                _unitOfWork.Atttach(item);

                GetSet().Remove(item);
            }
            else
                throw new NullReferenceException("Cannot remove a null entity");
        }

        public virtual void Modify(T item)
        {
            if (item != (T)null)
                _unitOfWork.Atttach<T>(item);
            else
                throw new NullReferenceException("Cannot modify a null entity");
        }

        public virtual T Get(Guid id)
        {
            if (id != null)
                return GetSet().Find(id);
            else
                return null;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return GetSet();
        }

        public virtual IEnumerable<T> GetFiltered(Expression<Func<T, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        

        #endregion

        #region IDisposable members

        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

        public void Merge(T persisted, T current)
        {
            _unitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region Private Methods

        IDbSet<T> GetSet()
        {
            return _unitOfWork.CreateSet<T>();
        }

        #endregion
    }
}
