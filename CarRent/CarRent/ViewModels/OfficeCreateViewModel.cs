using System.Collections.Generic;
using Entities;

namespace CarRent.ViewModels
{
    public class OfficeCreateViewModel
    {
        public int ID { get; set; }

        public int CityID { get; set; }

        public virtual City City { get; set; }

        public string PlaceDescription { get; set; }

        public List<City> Cities { get; set; }
    }
}