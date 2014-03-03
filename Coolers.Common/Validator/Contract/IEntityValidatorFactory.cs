using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Common.Validator.Contract
{
    public interface IEntityValidatorFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEntityValidator Create();
    }
}
