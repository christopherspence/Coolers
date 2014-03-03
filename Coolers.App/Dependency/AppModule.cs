using Coolers.Common.Validator.Contract;
using Coolers.Common.Validator.Implementation;
using Coolers.DAL.Contract;
using Coolers.DAL.Implementation;
using Coolers.Managers.Contract;
using Coolers.Managers.Implementation;
using Coolers.Repositories.Contract;
using Coolers.Repositories.Implementation;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.App.Dependency
{
    public class AppModule : NinjectModule
    {
        public override void Load()
        {
            // Common 
            Bind<IEntityValidator>().To<DataAnnotationsEntityValidator>();
            Bind<IEntityValidatorFactory>().To<DataAnnotationsEntityValidatorFactory>();

            // DAL
            Bind<IQueryableUnitOfWork>().To<UnitOfWork>();

            // Repos
            Bind<ICoolerRepository>().To<CoolerRepository>();
            Bind<IBeverageRepository>().To<BeverageRepository>();
            
            // Managers
            Bind<ICoolerManager>().To<CoolerManager>();
        }

        public object IEntityValidator { get; set; }
    }
}
