using Coolers.DAL.Contract;
using Coolers.DAL.Implementation;
using Coolers.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.DAL
{
    public class CoolerDBInitializer : DropCreateDatabaseAlways<UnitOfWork>
    {
        protected override void Seed(UnitOfWork context)
        {
            IList<Cooler> defaultCoolers = new List<Cooler>
            {
                new Cooler { Id = Guid.NewGuid(), Name = "Cooler 1", MaxCapacity = 20f, Iced = true, Sealed = true },
                new Cooler { Id = Guid.NewGuid(), Name = "Cooler 2", MaxCapacity = 5f, Sealed = true, Cryogenized = true }
            };

            foreach (Cooler defaultCooler in defaultCoolers)
                context.Coolers.Add(defaultCooler);
        }        
    }
}
