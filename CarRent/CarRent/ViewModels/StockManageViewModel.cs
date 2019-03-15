using Entities;
using System.Collections.Generic;
using CarRent.ViewModels;

namespace CarRent.ViewModels
{
    public class StockManageViewModel
    {
        public City City { get; set; }

        public IEnumerable<CarInStockListViewModel> CarsInStock { get; set; }
    }
}