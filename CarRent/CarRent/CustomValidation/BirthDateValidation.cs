using System;
using System.ComponentModel.DataAnnotations;
using CarRent.Models;

namespace CarRent.CustomValidation
{
    public class BirthDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as RegisterViewModel;

            if (model.BirthDate > DateTime.Now)
            {
                return new ValidationResult("Birth date cannot be in future");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}