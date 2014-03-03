using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Core
{
    public class InvalidBeverageException : Exception
    {
        public InvalidBeverageException(string message)
            : base(message)
        {
        }
    }
}
