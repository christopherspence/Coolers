using Coolers.Common.Validator.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Common.Validator.Implementation
{
    public class DataAnnotationsEntityValidator : IEntityValidator
    {
        #region Private methods

        void SetValidatableObjectErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            if (typeof(IValidatableObject).IsAssignableFrom(typeof(TEntity)))
            {
                ValidationContext context = new ValidationContext(item, null, null);

                IEnumerable<ValidationResult> results = ((IValidatableObject)item).Validate(context);

                errors.AddRange(results.Select(r => r.ErrorMessage));
            }
        }

        void SetValidationAttributeErrors<TEntity>(TEntity item, List<string> errors) where TEntity : class
        {
            IEnumerable<string> result = from property in TypeDescriptor.GetProperties(item).Cast<PropertyDescriptor>()
                         from attribute in property.Attributes.OfType<ValidationAttribute>()
                         where !attribute.IsValid(property.GetValue(item))
                         select attribute.FormatErrorMessage(string.Empty);

            if (result != null && result.Any())
                errors.AddRange(result);
        }

        #endregion

        public bool IsValid<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return false;

            List<string> validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);

            return !validationErrors.Any();
        }

        public IEnumerable<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class
        {
            if (item == null)
                return null;

            List<string> validationErrors = new List<string>();

            SetValidatableObjectErrors(item, validationErrors);
            SetValidationAttributeErrors(item, validationErrors);

            return validationErrors;
        }
    }
}
