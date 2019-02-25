using System.ComponentModel.DataAnnotations;
using CarRent.Models;

namespace CarRent.CustomValidation
{
    public class CheckDriverLicense : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as RegisterViewModel;           

            if (model.BirthDate.AddYears(16) > model.DrivingLicenseDate)
            {
                return new ValidationResult("You cannot get a driver's license if you are under 16");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}