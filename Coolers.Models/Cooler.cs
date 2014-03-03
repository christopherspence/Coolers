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
        public float MaxCapacity { get; set; }

        public bool Iced { get; set; }
        public bool Sealed { get; set; }
        public bool Cryogenized { get; set; }
   
        public IList<Beverage> Beverages { get; set; }  

        [NotMapped]
        public float CurrentCapacity
        {
            get
            {
                float currentCapacity = 0;

                foreach (Beverage beverage in this.Beverages)
                    currentCapacity += beverage.Size;

                return currentCapacity;
            }
        }

        public Cooler()
        {
            this.Beverages = new List<Beverage>();
        }

        public static Cooler Create(Guid id, string name, float maxCapacity, bool iced, bool isSealed, bool cryogenized)
        {
            return new Cooler
            {
                Id = id,
                MaxCapacity = maxCapacity,
                Iced = iced,
                Sealed = isSealed,
                Cryogenized = cryogenized
            };
        }        
         
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> validationErrors = new List<ValidationResult>();

            if (string.IsNullOrEmpty(this.Name))
                validationErrors.Add(new ValidationResult("Cooler Name is Required"));
            
            // Now check the cooler capacity
            if (this.CurrentCapacity > this.MaxCapacity)
                validationErrors.Add(new ValidationResult("Cooler Limit has been Exceeded"));

            // Make sure all the beverages are of the right type for this cooler.
            foreach (Beverage beverage in this.Beverages)
            {
                // iced and sealed beverages get a free pass in cryogenized coolers
                if (beverage.NeedsIced && this.Cryogenized)
                    continue;

                if (beverage.NeedsIced && !this.Iced)
                    validationErrors.Add(new ValidationResult(String.Format("{0} - Iced Beverages aren't supported in this cooler", beverage.Name)));
                else if (beverage.NeedsSealed && !this.Sealed)
                    validationErrors.Add(new ValidationResult(String.Format("{0} - Iced Beverages aren't supported in this cooler", beverage.Name)));
                else if (beverage.NeedsCryogenized && !this.Cryogenized)
                    validationErrors.Add(new ValidationResult(String.Format("{0} - Cryogenized Beverages aren't supported in this cooler", beverage.Name)));
                  
            }

            return validationErrors;

        }
    }
}
