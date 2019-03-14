using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Entities;

namespace CarRent.ViewModels
{
    public class OfficeInCityCiewModel
    {
        public List<Office> Offices { get; set; }

        public City City { get; set; }
    }
}