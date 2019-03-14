using System;
using System.ComponentModel.DataAnnotations;
using CarRent.ViewModels;

namespace CarRent.CustomValidation
{
    public class RentDateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as OrderCreateViewModel;

            //var modelStartDate = (DateTime)model.RentStartDate;
            //DateTime nowDate = new DateTime(modelStartDate.Year, modelStartDate.Month, modelStartDate.Day);

            DateTime nowDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            if (model.RentStartDate < nowDate)
            {
                return new ValidationResult("Rent start date cannot be in past");
            }
            else if(model.RentFinishDate < model.RentStartDate)
            {
                return new ValidationResult("Rent finish date cannot be earlier than start date");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}