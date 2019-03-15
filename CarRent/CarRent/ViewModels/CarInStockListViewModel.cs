using Entities;

namespace CarRent.ViewModels
{
    public class CarInStockListViewModel
    {
        public int ID { get; set; }

        public Car Car { get; set; }
        
        public int Amount { get; set; }

        public int AmountRented { get; set; }
    }
}