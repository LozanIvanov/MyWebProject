using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Core.CustomAttribute
{
    public class IsBiforeAttribute:ValidationAttribute
    {
        private readonly string propertyToCampare;

        public IsBiforeAttribute(string _propertyToCampare,string errorMessage="")
        {
            this.propertyToCampare = _propertyToCampare;
            this.ErrorMessage = errorMessage;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            try
            {
                DateTime dateToCompare = (DateTime)validationContext.
               ObjectType.GetProperty(propertyToCampare)
               .GetValue(validationContext.ObjectInstance);

                if((DateTime)value < dateToCompare)
                {
                    return ValidationResult.Success;
                
                        }
            }
            catch (Exception)
            {

            }
            return new ValidationResult(ErrorMessage);
           
        }
    }
}
