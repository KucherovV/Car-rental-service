using System;

namespace Entities
{
    public class Order
    {
        public int ID { get; set; }

        public string UserID { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CarID { get; set; }

        public virtual Car Car { get; set; }

        public string AdditionalOptionsJson { get; set; }

        public DateTime OrderDateTime { get; set; }

        public DateTime RentStartDateTime { get; set; }

        public DateTime RentFinishDateTime { get; set; }

        public int OfficeIdStart { get; set; }

        public virtual Office OfficeStart { get; set; }

        public int OfficeIdEnd { get; set; }

        public virtual Office OfficeEnd { get; set; }

        public string Comment { get; set; }

        public int Price { get; set; }

        public string Status { get; set; }

        public int? StockID { get; set; }
    }
}
