using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRent.ViewModels
{
    public class OrderListViewModel
    {
        public MultiSelectList Statuses { get; set; }

        public List<string> SelectedStatuses { get; set; }
    }
}