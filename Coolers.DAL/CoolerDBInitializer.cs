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
                new Cooler { Name = "Cooler 1", SizeInMilliliters = 20f, CoolerOptions = new List<CoolerOption> { CoolerOption.Iced, CoolerOption.Sealed } },
                new Cooler { Name = "Cooler 2", SizeInMilliliters = 5f, CoolerOptions = new List<CoolerOption> { CoolerOption.Sealed, CoolerOption.Cryonized } }
            };

            foreach (Cooler defaultCooler in defaultCoolers)
                context.Coolers.Add(defaultCooler);
        }        
    }
}
