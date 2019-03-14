using System.ComponentModel.DataAnnotations;
using Entities;
using System.Web.Mvc;
using System.Collections.Generic;
using CarRent.ViewModels;
using CarRent.CustomValidation;

namespace CarRent.ViewModels
{
    public class CarUserIndexViewModel
    {
        public List<CarUserListItemViewModel> Items { get; set; }   

        public SelectList Sortings { get; set; }

        public string SortBy { get; set; }

        public MultiSelectList Grades { get; set; }

        public List<string> SelectedGrades { get; set; }

        [Display(Name = "Engine Types")]
        public MultiSelectList EngineTypes { get; set; }

        public List<string> SelectedEngineTypes { get; set; }

        public MultiSelectList Brands { get; set; }

        public List<string> SelectedBrands { get; set; }

        [Required(ErrorMessage = "Enter min price")]
        [Range(0, 1000, ErrorMessage = "Range in 0$ and 1000$")]
        public int MinPricePerDay { get; set; }

        [Range(0, 1000, ErrorMessage = "Range in 0$ and 1000$")]
        [Required(ErrorMessage = "Enter max price")]
        public int MaxPricePerDay { get; set; }

        public SelectList Cities { get; set; }

        public int CityID { get; set; }
    }
}