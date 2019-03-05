using System.ComponentModel.DataAnnotations;

namespace CarRent.ViewModels
{
    public class CarPricingViewModel
    {
        [Required]
        public int CarID { get; set; }

        [Required]
        [Display(Name = "1 day")]
        [Range(10, 1000, ErrorMessage = "Price must be between 10$ and 1000$")]
        public int PricePer1Day { get; set; }

        [Required]
        [Display(Name = "< 3 days")]
        [Range(10, 1000, ErrorMessage = "Price must be between 10$ and 1000$")]
        public int PricePer3Days { get; set; }

        [Required]
        [Display(Name = "< 1 week")]
        [Range(10, 1000, ErrorMessage = "Price must be between 10$ and 1000$")]
        public int PricePer7Days { get; set; }

        [Required]
        [Display(Name = "< 2 weeks")]
        [Range(10, 1000, ErrorMessage = "Price must be between 10$ and 1000$")]
        public int PricePer14Days { get; set; }

        [Required]
        [Display(Name = "< 1 month")]
        [Range(10, 1000, ErrorMessage = "Price must be between 10$ and 1000$")]
        public int PricePerMonth { get; set; }

        [Required]
        [Display(Name = "> 1 month")]
        [Range(10, 1000, ErrorMessage = "Price must be between 10$ and 1000$")]
        public int PricePerMoreThanMonth { get; set; }

        [Required]
        [Display(Name = "Car Name")]
        public string CarName { get; set; }
    }
}