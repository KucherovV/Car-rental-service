using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entities;

namespace CarRent.ViewModels
{
    public class UserDetailsViewModel
    {
        [Required]
        public string UserID { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Surname")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "ID Number")]
        public string IDNumber { get; set; }

        [Required]
        [Display(Name = "TAX ID Number")]
        public string TaxID { get; set; }

        [Required]
        [Display(Name = "Birth Date")]
        public string BirthDate { get; set; }

        [Required]
        [Display(Name = "Driver's License Date")]
        public string DrivingLicenseDate { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        public Blocking Blocking { get; set; }

        // orders

        //warnings


    }
}