using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Common
{
    public class ApplicationValidationErrorsException : Exception
    {
        #region Properties

        IEnumerable<string> _validationErrors;

        public IEnumerable<string> ValidationErrors
        {
            get
            {
                return _validationErrors;
            }
        }

        #endregion

        #region Constructors

        public ApplicationValidationErrorsException(IEnumerable<string> validationErrors)
            : base()
        {
            _validationErrors = validationErrors;
        }

        #endregion
    }
}
