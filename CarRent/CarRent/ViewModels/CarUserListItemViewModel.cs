using Entities;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace CarRent.ViewModels
{
    public class CarUserListItemViewModel
    {
        public int ID { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }
        
        public int CityID { get; set; }

        public string Grade { get; set; }

        [Display(Name = "Passangers Count")]
        public int PassangerCount { get; set; }

        [Display(Name = "Luggage count")]
        public int LuggageCount { get; set; }

        [Display(Name = "Fuel Consumption")]
        public int FuelConsumption { get; set; }

        [Display(Name = "Door Count")]
        public int DoorCount { get; set; }

        [Display(Name = "Engine Type")]
        public string EngineType { get; set; }

        [Display(Name = "Transmission Type")]
        public string TransmissionType { get; set; }

        [Display(Name = "Has Air Conditioning")]
        public bool HasAirConditioning { get; set; }

        public int OrdersCount { get; set; }

        [Display(Name = "Image")]
        public string FileName { get; set; }

        public CarTimePricing CarTimePricing { get; set; }

        public Dictionary<string, string> IconDescription { get; set; }

        public bool IsInStock { get; set; }

        public DateTime? WillBeAviable { get; set; }

        public bool IsBusy { get; set; }
    }
}