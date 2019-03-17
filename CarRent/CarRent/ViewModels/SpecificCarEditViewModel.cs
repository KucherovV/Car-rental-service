using System.ComponentModel.DataAnnotations;
using Entities;
using CarRent.CustomValidation;

namespace CarRent.ViewModels
{
    public class SpecificCarEditViewModel
    {
        public int ID { get; set; }

        public virtual City City { get; set; }

        public int CityID { get; set; }

        public virtual Car Car { get; set; }

        public int CarID { get; set; }

        [Required]
        [UniqueVIN]
        [StringLength(17, MinimumLength = 17, ErrorMessage = "VIN must be 17 characters")]
        public string VIN { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "RegisterNumber must be between 5 and 10 characters")]
        [Display(Name = "Registration Number")]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Color must be between 2 and 15 characters")]
        public string Color { get; set; }

        [StringLength(100, MinimumLength = 5, ErrorMessage = "Defects lenght must be between 5 and 100 characters")]
        public string Defects { get; set; }

        public bool IsEditing { get; set; }
    }
}