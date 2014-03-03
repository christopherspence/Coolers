using Coolers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Managers.Contract
{
    public interface ICoolerManager
    {
        IList<Cooler> GetAllCoolers();

        Cooler AddCooler(Cooler cooler);
        void UpdateCooler(Cooler cooler);
    }
}
