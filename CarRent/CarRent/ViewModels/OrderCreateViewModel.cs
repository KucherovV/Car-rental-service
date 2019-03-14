using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CarRent.CustomValidation;

namespace CarRent.ViewModels
{
    public class OrderCreateViewModel
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        [Required]
        [Display(Name = "Choose car")]
        public int CarID { get; set; }

        public virtual Car Car { get; set; }

        public string AdditionalOptionsJson { get; set; }

        public DateTime OrderDateTime { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [Display(Name = "Start date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{DD-MM-YYYY HH:mm}")]
        [RentDateValidation]
        public DateTime? RentStartDate { get; set; }  

        [Required(ErrorMessage = "Finish date is required")]
        [Display(Name = "Finish date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{DD-MM-YYYY HH:mm}")]
        public DateTime? RentFinishDate { get; set; }

        [Required(ErrorMessage = "Start office is required")]
        [Display(Name = "Start office")]
        public int OfficeIdStart { get; set; }

        public virtual Office OfficeStart { get; set; }

        [Required(ErrorMessage = "End office is required")]
        [Display(Name = "End office")]
        public int OfficeIdEnd { get; set; }

        public virtual Office OfficeEnd { get; set; }

        public string Comment { get; set; }

        public SelectList Cars { get; set; }

        public SelectList Offices { get; set; }

        public List<AdditionalOption> AdditionalOptions { get; set; }

        public List<bool> OptionsSelected { get; set; }

        public CarTimePricing CarTimePricing { get; set; }
    }
}