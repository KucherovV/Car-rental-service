using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Entities;
using static Entities.Enumerations;

namespace CarRent.ViewModels
{
    public class CarCreateViewModel
    {
        public SelectList BrandsList { get; set; }

        [Display(Name = "Engine Type")]
        public SelectList EngineTypes { get; set; }

        [Display(Name = "Transmission Type")]
        public SelectList TransmissionTypes { get; set; }

        public string Brand { get; set; }

        public int CarID { get; set; }

        [Required(ErrorMessage = "Model field is required")]
        [StringLength(20, ErrorMessage = "Model name must be less than 20 characters", MinimumLength = 2)]
        public string Model { get; set; }

        [Required(ErrorMessage = "Passanger Count field is required")]
        [Display(Name = "Passangers Count")]
        [Range(1, 8, ErrorMessage = "Passangers count must be between 1 and 8")]
        public int PassangerCount { get; set; }

        [Required(ErrorMessage = "Luggage Count field is required")]
        [Display(Name = "Luggage count")]
        [Range(1, 10, ErrorMessage = "Luggage count must be between 1 and 10")]
        public int LuggageCount { get; set; }

        [Required(ErrorMessage = "Fuel Consumption field is required")]
        [Display(Name = "Fuel Consumption")]
        [Range(0, 20, ErrorMessage = "Fuel consumption must be between 0 and 20")]
        public int FuelConsumption { get; set; }

        [Required(ErrorMessage = "Door Count field is required")]
        [Display(Name = "Door Count")]
        [Range(3, 5, ErrorMessage = "Door count should be between 3 and 5")]
        public int DoorCount { get; set; }      

        [Required]
        [Display(Name = "Has Air Conditioning")]
        public bool HasAirConditioning { get; set; }

        [Display(Name = "Image")]
        public string FileName { get; set; }

        public string SelectedEngineType { get; set; }

        public string SelectedTransmissionType { get; set; }
    }
}