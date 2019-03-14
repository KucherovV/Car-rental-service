using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarRent.ViewModels
{
    public class ManagerOrderViewModel
    {
        public int ID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Car Car { get; set; }

        [Display(Name = "Options")]
        public List<AdditionalOption> AdditionalOptions { get; set; }

        [Display(Name = "Order DateTime")]
        public DateTime OrderDateTime { get; set; }

        [Display(Name = "Rent Start Date")]
        public DateTime RentStartDateTime { get; set; }

        [Display(Name = "Rent Finish Date")]
        public DateTime RentFinishDateTime { get; set; }

        [Display(Name = "Start Point")]
        public virtual Office OfficeStart { get; set; }

        [Display(Name = "Finish Point")]
        public virtual Office OfficeEnd { get; set; }

        public string Status { get; set; }

        public string Comment { get; set; }

        public int Price { get; set; }

        public string Color { get; set; }
    }
}