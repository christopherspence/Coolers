using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Models
{
    public class Beverage : Entity, IValidatableObject
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float Size { get; set; }

        public bool NeedsSealed { get; set; }
        public bool NeedsIced { get; set; }
        public bool NeedsCryogenized { get; set; }

        public Cooler Cooler { get; set; }

        public static Beverage Create(Guid id, string name, float size, bool needsSealed, bool needsIced, bool needscryogenized)
        {
            return new Beverage
            {
                Id = id,
                Name = name,
                Size = size,
                NeedsIced = needsIced,
                NeedsSealed = needsSealed,
                NeedsCryogenized = needscryogenized
            };
        }

        #region IValidatable methods
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (this.Id == null)
                validationResults.Add(new ValidationResult("Beverage requires an id"));
            if (string.IsNullOrEmpty(this.Name))
                validationResults.Add(new ValidationResult("Beverage name cannot be empty"));

            return validationResults;
        }
        
        #endregion
    }
}
