using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Car
    {
        public int ID { get; set; }

        public bool Archived { get; set; }

        //public int BrandID { get; set; }

        //[Display(Name = "Brand")]
        //public virtual Brand Brand { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

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

        [Display(Name = "Image")]
        public string FileName { get; set; }
    }
}
