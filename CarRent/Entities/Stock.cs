﻿namespace Entities
{
    public class Stock
    {
        public int ID { get; set; }

        public int CityID { get; set; }

        public int CarID { get; set; }

        public virtual Car Car { get; set; }


        public int Amount { get; set; }
    }
}
