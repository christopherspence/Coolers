using Coolers.DAL.Contract;
using Coolers.Models;
using Coolers.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Repositories.Implementation
{
    public class BeverageRepository : Repository<Beverage>, IBeverageRepository
    {
        public BeverageRepository(IQueryableUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
    }
}
