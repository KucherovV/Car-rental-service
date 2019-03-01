using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRent.ViewModels
{
    public class UserIndexViewModel
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public string BirthDate { get; set; }

        [Required]
        [Display(Name = "Driving License Date")]
        public string DrivingLicenseDate { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsBlocked { get; set; }
    }
}