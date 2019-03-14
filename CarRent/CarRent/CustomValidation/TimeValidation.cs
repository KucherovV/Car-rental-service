using System;
using System.ComponentModel.DataAnnotations;
using CarRent.ViewModels;

namespace CarRent.CustomValidation
{
    public class TimeValidation : ValidationAttribute
    {
        //protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //{
        //    var model = validationContext.ObjectInstance as OrderCreateViewModel;
        //    DateTime time;

        //    try
        //    {
        //        time = DateTime.Parse(model.StartTime);
        //        return ValidationResult.Success;
        //    }
        //    catch
        //    {
        //        return new ValidationResult("Wrong time format");
        //    }
        //}
    }
}