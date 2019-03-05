using System.Collections.Generic;

namespace Entities
{
    public static class Enumerations
    {
        public static List<string> EngineTypes
        {
            get
            {
                return new List<string>()
                {
                    "Petrol",
                    "Gas",
                    "Hybrid",
                    "Electro"
                };
            }
        }

        public static List<string> TransmissionTypes
        {
            get
            {
                return new List<string>()
                {
                    "Automatic",
                    "Manual",
                    "SemiAutomatic"
                };
            }
        }

        public static List<string> Brands
        {
            get
            {
                return new List<string>()
                {
                    "Ford",
                    "Dodge",
                    "Chevrolet",
                    "Ferrari",
                    "Porche",
                    "Audi",
                    "Daewoo",
                    "Honda",
                    "Hyndai",
                    "Kia",
                    "Mazda",
                    "Mersedes-Benz",
                    "Mitsubishi",
                    "Nissan",
                    "Opel",
                    "Peugeot",
                    "Renault",
                    "Skoda",
                    "Toyota",
                    "Volksvagen",
                    "Vaz",
                    "Zaz",
                    "Acura",
                    "Alfa Romeo",
                    "Aston Martin",
                    "Bentley",
                    "Bmw",
                    "Cadillac",
                    "Cherry",
                    "Chrysler",
                    "Citroen",
                    "Dacia",
                    "Fiat",
                    "Gelly",
                    "Great Wall",
                    "Infiniti",
                    "JAC",
                    "Jaguar",
                    "JCB",
                    "Jeep",
                    "Lada",
                    "Lamborghini",
                    "Lancia",
                    "Land Rover",
                    "Lexus",
                    "Maserati",
                    "MINI",
                    "Seat",
                    "Smart",
                    "Subaru",
                    "Suzuki",
                    "Tesla",
                    "Toyota",
                    "Volvo",
                };
            }
        }

        public static List<string> Grades
        {
            get
            {
                return new List<string>()
                {
                    "Cheap",
                    "Comfort",
                    "Premium",
                    "Suv",
                    "Sport",
                    "Classic",
                    "Sport Classic"
                };
            }
        }
    }
}
