using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataBase;
using CarRent.ViewModels;

namespace CarRent.Controllers
{
    public class UserController : Controller
    {
        public ActionResult CarList(string search, 
            List<string> SelectedBrands,
            string SortBy, 
            List<string> SelectedGrades, 
            List<string> SelectedEngineTypes, 
            int? MinPricePerDay, 
            int? MaxPricePerDay)
        {
            //get list of all cars
            IEnumerable<Car> cars = DB.GetList<Car>().Where(c => c.Archived == false);
            var carsInDb = cars;

            //filter by search string
            if (!string.IsNullOrEmpty(search))
            {
                cars = cars.Where(c => c.Brand.ToLower().Contains(search) || c.Model.ToLower().Contains(search));               
            }
           
            cars = cars.ToList();
            var viewModelItems = new List<CarUserListItemViewModel>();
            var viewModel = new CarUserIndexViewModel();

            //initialize viewModel
            viewModel.Sortings = new SelectList(new List<string>()
            {
                "Default",
                "Price Low",
                "Price High",
                "Name"
            });
            viewModel.Grades = new MultiSelectList(Enumerations.Grades);
            viewModel.EngineTypes = new MultiSelectList(Enumerations.EngineTypes);

            //if cars exist
            if (cars.Count() != 0)
            {
                //filter by brand

                if (SelectedBrands != null)
                {
                    var tempCars = new List<Car>();
                    foreach (var item in cars)
                    {
                        if (SelectedBrands.Contains(item.Brand))
                            tempCars.Add(item);
                    }
                    cars = tempCars;
                }
                foreach (var car in cars)
                {
                    viewModelItems.Add(new CarUserListItemViewModel
                    {
                        Brand = car.Brand,
                        DoorCount = car.DoorCount,
                        EngineType = car.EngineType,
                        FileName = car.FileName,
                        FuelConsumption = car.FuelConsumption,
                        Grade = car.Grade,
                        HasAirConditioning = car.HasAirConditioning,
                        LuggageCount = car.LuggageCount,
                        Model = car.Model,
                        PassangerCount = car.PassangerCount,
                        TransmissionType = car.TransmissionType,
                        CarTimePricing = (DB.GetList<CarTimePricing>().ToList()).SingleOrDefault(cp => cp.CarID == car.ID) ?? new CarTimePricing()
                    });
                }

                //sort 
                switch (SortBy)
                {
                    case "Price Low":
                        {
                            viewModelItems = viewModelItems.OrderBy(vm => vm.CarTimePricing.PricePer1Day).ToList();
                        }
                        break;

                    case "Price High":
                        {
                            viewModelItems = viewModelItems.OrderByDescending(vm => vm.CarTimePricing.PricePer1Day).ToList();
                        }
                        break;

                    case "Name":
                        {
                            viewModelItems = viewModelItems.OrderBy(vm => vm.Brand).ToList();
                        }
                        break;
                }
              
                //filter by grades
                if (SelectedGrades != null)
                {
                    var temp = new List<CarUserListItemViewModel>();
                    foreach (var item in viewModelItems)
                    {
                        if (SelectedGrades.Contains(item.Grade))
                            temp.Add(item);
                    }
                    viewModelItems = temp;
                }

                //filter by engineType
                if (SelectedEngineTypes != null)
                {
                    var temp = new List<CarUserListItemViewModel>();
                    foreach (var item in viewModelItems)
                    {
                        if (SelectedEngineTypes.Contains(item.EngineType))
                            temp.Add(item);
                    }
                    viewModelItems = temp;
                }

                //set price range if null
                if (MinPricePerDay == null)
                {
                    var pricing = DB.GetList<CarTimePricing>().ToList();
                    viewModel.MaxPricePerDay = pricing.Max(p => p.PricePer1Day);

                    MinPricePerDay = 0;
                    MaxPricePerDay = viewModel.MaxPricePerDay;
                }
                //check for error
                else
                {
                    if(MinPricePerDay > MaxPricePerDay)
                    {
                        ModelState.AddModelError("MinPricePerDay", "Min price cannot be higher than max");
                        viewModel.Items = viewModelItems;

                        var brands1 = new List<string>();
                        foreach (var item in carsInDb)
                        {
                            brands1.Add(item.Brand);
                        }
                        viewModel.Brands = new MultiSelectList(brands1.Distinct().ToList());

                        return View(viewModel);
                    }
                }

                //filter by price
                var temp1 = new List<CarUserListItemViewModel>();
                foreach(var item in viewModelItems)
                {
                    if(item.CarTimePricing.PricePer1Day >= MinPricePerDay && item.CarTimePricing.PricePer1Day <= MaxPricePerDay)
                    {
                        temp1.Add(item);
                    }
                }
                viewModelItems = temp1;           
            }

            viewModel.Items = viewModelItems;

            //filter distinct brands
            var brands = new List<string>();
            foreach(var item in carsInDb)
            {
                brands.Add(item.Brand);
            }
            viewModel.Brands = new MultiSelectList(brands.Distinct().ToList());

            return View(viewModel);
        }
    }
}