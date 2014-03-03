using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coolers.Models
{
    public class Cooler : Entity, IValidatableObject
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float SizeInMilliliters { get; set; }
        public IEnumerable<CoolerOption> CoolerOptions { get; set; }
        public IEnumerable<Beverage> Beverages { get; set; }

        [NotMapped]
        public float CurrentCapacity
        {
            get
            {
                float currentCapacity = 0;

                foreach (Beverage beverage in this.Beverages)
                    currentCapacity += beverage.SizeInMilliliters;

                return currentCapacity;
            }
        }
        
        [NotMapped]
        public bool IsIced
        {
            get
            {
                foreach (CoolerOption option in this.CoolerOptions)
                {
                    if (option == CoolerOption.Iced)
                        return true;
                }

                return false;
            }
        }

        [NotMapped]
        public bool IsSealed
        {
            get
            {
                foreach (CoolerOption option in this.CoolerOptions)
                {
                    if (option == CoolerOption.Sealed)
                        return true;
                }

                return false;
            }
        }

        [NotMapped]
        public bool IsCryonized
        {
            get
            {
                foreach (CoolerOption option in this.CoolerOptions)
                {
                    if (option == CoolerOption.Cryonized)
                        return true;
                }

                return false;
            }
        }
   
        public Cooler()
        {
            this.CoolerOptions = new List<CoolerOption>();
            this.Beverages = new List<Beverage>();
        }
         
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(Name))
                validationErrors.Add(new ValidationResult(Messages.BeverageNameEmpty));
            
            // Now check the cooler capacity
            if (this.CurrentCapacity > this.SizeInMilliliters)
                validationErrors.Add(new ValidationResult(Messages.CoolerLimitExceeded));

            // Make sure all the beverages are of the right type for this cooler.
            foreach (Beverage beverage in this.Beverages)
            {
                if (beverage.NeedsIced && !this.IsIced)
                    validationErrors.Add(new ValidationResult(Messages.IcedNotSupported));
                else if (beverage.NeedsSealed && !this.IsSealed)
                    validationErrors.Add(new ValidationResult(Messages.SealedNotSupported));
                else if (beverage.NeedsCryonized && !this.IsCryonized)
                    validationErrors.Add(new ValidationResult(Messages.CryoNotSupported));
            }

            return validationErrors;

        }
    }
}
