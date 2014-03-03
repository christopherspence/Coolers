using Coolers.DAL.Contract;
using Coolers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.DAL.Implementation
{
    public class UnitOfWork :  DbContext, IQueryableUnitOfWork
    {
        #region IDbSet members

        IDbSet<Beverage> _beverages;
        public IDbSet<Beverage> Beverages
        {
            get
            {
                if (_beverages == null)
                    _beverages = base.Set<Beverage>();

                return _beverages;
            }
        }

        IDbSet<Cooler> _coolers;
        public IDbSet<Cooler> Coolers
        {
            get
            {
                if (_coolers == null)
                    _coolers = base.Set<Cooler>();

                return _coolers;
            }
        }
    
        #endregion

        public UnitOfWork()
            : base("name=Coolers.DAL.UnitOfWork")
        {
            // Fill out the default coolers
            Database.SetInitializer<UnitOfWork>(new CoolerDBInitializer());
        }


        #region IUnitOfWork methods

        public DbSet<T> CreateSet<T>() where T : class
        {
            return base.Set<T>();
        }

        public void Atttach<T>(T item) where T : class
        {
            // Attach and set as unmodified
            base.Entry<T>(item).State = EntityState.Unchanged;
        }

        public void SetModified<T>(T item) where T : class
        {
            // attach and set as unmodified
            base.Entry<T>(item).State = EntityState.Modified;
        }

        public void ApplyCurrentValues<T>(T original, T current) where T : class
        {
            base.Entry<T>(original).CurrentValues.SetValues(current);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void CommitAndRefreshChanges()
        {
            bool saveFailed = false;

            do
            {
                try
                {
                    base.SaveChanges();

                    saveFailed = false;

                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    ex.Entries.ToList()
                              .ForEach(entry =>
                              {
                                  entry.OriginalValues.SetValues(entry.GetDatabaseValues());
                              });

                }
            } while (saveFailed);
        }

        public void RollbackChanges()
        {
            // Set everything back to unchanged
            base.ChangeTracker.Entries()
                              .ToList()
                              .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        #endregion

        #region IQueryableUnitOfWork methods

        public IEnumerable<T> ExecuteQuery<T>(string query, params object[] parameters)
        {
           return base.Database.SqlQuery<T>(query, parameters);
        }

        public int ExecuteCommand(string sqlCommand, params object[] parameters)
        {
            return base.Database.ExecuteSqlCommand(sqlCommand, parameters);
        }

        #endregion
    }
}
