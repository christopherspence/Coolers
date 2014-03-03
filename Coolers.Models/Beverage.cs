using Coolers.Core;
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
        public float SizeInMilliliters { get; set; }
        public IEnumerable<CoolerOption> CoolerRequirements { get; set; }

        public Cooler Cooler { get; set; }

        [NotMapped]
        public bool NeedsSealed
        {
            get
            {
                foreach (CoolerOption option in this.CoolerRequirements)
                {
                    if (option == CoolerOption.Sealed)
                        return true;
                }

                return false;
            }
        }

        [NotMapped]
        public bool NeedsIced
        {
            get
            {
                foreach (CoolerOption option in this.CoolerRequirements)
                {
                    if (option == CoolerOption.Iced)
                        return true;
                }

                return false;
            }
        }

        [NotMapped]
        public bool NeedsCryonized
        {
            get
            {
                foreach (CoolerOption option in this.CoolerRequirements)
                {
                    if (option == CoolerOption.Cryonized)
                        return true;
                }

                return false;
            }
        }




        #region IValidatable methods
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationResults = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Name))
                validationResults.Add(new ValidationResult(Messages.BeverageNameEmpty));

            return validationResults;
        }
        
        #endregion
    }
}
