using Entities;
using System.Collections.Generic;

namespace CarRent.ViewModels
{
    public class StockManageViewModel
    {
        public City City { get; set; }

        public IEnumerable<Stock> CarsInStock { get; set; }
    }
}