namespace Entities
{
    public class Car
    {
        public int ID { get; set; }

        public int BrandID { get; set; }

        public Brand Brand { get; set; }

        public string Model { get; set; }

        public int CityID { get; set; }

        public City City { get; set; }

        public int PassangerCount { get; set; }

        public int LuggageCount { get; set; }

        public decimal FuelConsumption { get; set; }

        public int DoorCount { get; set; }

        public string EngineType { get; set; }

        public string TransmissionType { get; set; }

        public decimal EngineCapacity { get; set; }

        public bool HasAirConditioning { get; set; }

    }
}
