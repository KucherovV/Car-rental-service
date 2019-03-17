using System.ComponentModel.DataAnnotations;
using Entities;
using DataBase;
using CarRent.ViewModels;
using System.Linq;

namespace CarRent.CustomValidation
{
    public class UniqueVIN : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = validationContext.ObjectInstance as SpecificCarEditViewModel;
            var stock = DB.GetList<Stock>().SingleOrDefault(s => s.VIN == model.VIN);

            if (!model.IsEditing)
            {


                if (stock != null)
                {
                    return new ValidationResult("VIN is not unique");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}