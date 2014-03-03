using Coolers.App.Dependency;
using Coolers.Common;
using Coolers.Managers.Contract;
using Coolers.Models;
using Ninject;
using Ninject.Activation.Blocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.App
{
    class Program
    {
        private static IKernel kernel;
        private static ICoolerManager manager;
        private static IList<Cooler> coolers;
        private static List<Beverage> beverages;

        static Program()
        {
            kernel = new StandardKernel(new AppModule());
        }

        static void DisplayCoolerInfo()
        {
            foreach (Cooler cooler in coolers)
                DisplayCoolerInfo(cooler);
        }

        static void DisplayCoolerInfo(Cooler cooler)
        {
            Console.WriteLine("Id: {0}\nName: {1}\nCurrentCapacity: {2}ml\nMax Capacity: {3}ml\nIsIced: {4}\nIsSealed: {5}\nIsCryonized: {6}\n\n",
                    cooler.Id,
                    cooler.Name,
                    cooler.CurrentCapacity,
                    cooler.MaxCapacity,
                    cooler.Iced,
                    cooler.Sealed,
                    cooler.Cryogenized);
        }

        static void GetAndDisplayCoolers()
        {
            Console.WriteLine("Getting Coolers");

           coolers = manager.GetAllCoolers().OrderBy(c => c.Name).ToList();

            // Output the result            
            Console.WriteLine("-----Coolers before Beverages -----");

            DisplayCoolerInfo();
        }

        static void CreateAndDisplayBeverages()
        {
            Console.WriteLine("Creating beverages");

            beverages = new List<Beverage>
            {
                new Beverage
                {
                    Id = Guid.NewGuid(),
                    Name = "Beverage 1",
                    NeedsIced = true,
                    Size = 10f
                },
                new Beverage
                {
                      Id = Guid.NewGuid(),
                      Name = "Beverage 2",
                      Size = 10f,
                      NeedsIced = true
                },
                new Beverage
                {
                    Id = Guid.NewGuid(),
                    Name = "Beverage 3",
                    Size = 5f,
                    NeedsIced = true,
                    NeedsSealed = true
                }
            };

            Console.WriteLine("----Beverages to be added---");

            foreach (Beverage beverage in beverages)
                Console.WriteLine("Id: {0}\nName: {1}\nSize: {2}ml\nNeedsIced:{3}\nNeedsSealed: {4}\nNeedsCryonized: {5}\n",
                    beverage.Id,
                    beverage.Name,
                    beverage.Size,
                    beverage.NeedsIced,
                    beverage.NeedsSealed,
                    beverage.NeedsCryogenized);
        }

        static void InsertBeveragesIntoCoolers()
        {
           
            foreach (Cooler cooler in coolers)
            {
                Console.WriteLine("\nAdding Beverages to {0}", cooler.Name);
                foreach (Beverage beverage in beverages)
                    cooler.Beverages.Add(beverage);

                DisplayCoolerInfo(cooler);

                try
                {
                    manager.UpdateCooler(cooler);
                }
                catch (ApplicationValidationErrorsException e)
                {
                    Console.WriteLine("\nError adding beverage:");
                    foreach (string message in e.ValidationErrors)
                        Console.WriteLine(message);
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Setting up deps");
            IActivationBlock block = kernel.BeginBlock();

           manager = block.Get<ICoolerManager>();

            GetAndDisplayCoolers();
            CreateAndDisplayBeverages();
            InsertBeveragesIntoCoolers();

            Console.ReadLine();


        }
    }
}
