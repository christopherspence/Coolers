using Coolers.Common.Validator.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Common.Validator.Implementation
{
    public class DataAnnotationsEntityValidatorFactory : IEntityValidatorFactory
    {
        public IEntityValidator Create()
        {
            return new DataAnnotationsEntityValidator();
        }
    }
}
