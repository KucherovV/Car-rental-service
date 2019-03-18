using CarRent.ViewModels;
using DataBase;
using Entities;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarRent.VO;
using PagedList;

namespace CarRent.Controllers
{
    public class UserController : Controller
    {
        private readonly DB DB;

        public UserController(DB Db)
        {
            DB = Db;
        }
        public UserController()
        {
            DB = new DB();
        }

        UserControllerVO vo = new UserControllerVO();

        public ActionResult CarList()
        {
            var pricing = DB.GetList<CarTimePricing>().ToList();

            IEnumerable<Car> cars = DB.GetList<Car>().Where(c => c.Archived == false);
            var carsInDB = new List<string>();
            foreach (var car in cars)
            {
                carsInDB.Add(car.Brand);
            }

            CarUserIndexViewModel viewModel = new CarUserIndexViewModel()
            {
                Sortings = new SelectList(new List<string>()
                {
                    "Default",
                    "Price Low",
                    "Price High",
                    "Name",
                    "Popularity"
                }),
                Grades = new MultiSelectList(Enumerations.Grades),
                EngineTypes = new MultiSelectList(Enumerations.EngineTypes),
                MinPricePerDay = 0,
                MaxPricePerDay = pricing.Max(p => p.PricePer1Day),
                Brands = new SelectList(carsInDB.Distinct()),
                Cities = new SelectList(DB.GetList<City>(), "ID", "Name")
            };

            vo.CarIndexShown();
            return View(viewModel);
        }

        public ActionResult GetCars(string search,
            int? CityID,
            List<string> SelectedBrands,
            string SortBy,
            List<string> SelectedGrades,
            List<string> SelectedEngineTypes,
            int? MinPricePerDay,
            int? MaxPricePerDay)
        {
            IEnumerable<Car> cars = DB.GetList<Car>().Where(c => c.Archived == false);
            
            var items = new List<CarUserListItemViewModel>().ToList();

            if (CityID == null)
            {
                var cities = DB.GetList<City>().ToList();
                if (cities != null)
                {
                    CityID = cities.First().ID;
                }
            }

            if (!string.IsNullOrEmpty(search))
            {
                cars = cars.Where(c => c.Brand.ToLower().Contains(search) || c.Model.ToLower().Contains(search));
            }
            cars = cars.ToList();

            if (cars.Count() != 0)
            {
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
                    var freeCarInStock = DB.GetList<Stock>()
                        .FirstOrDefault(s => s.CityID == CityID && s.CarID == car.ID && s.IsBusy == false) as Stock;

                    bool inStock;
                    DateTime? abilityTime = null;

                    if (freeCarInStock == null)
                    {
                        inStock = false;

                        var temp = DB.GetList<Order>()
                                    .Where(o => o.CarID == car.ID
                                        && o.Stock.CityID == (int)CityID)
                                    .Select(o => o.RentFinishDateTime).ToList();
                        if (temp.Count != 0)
                            abilityTime = temp.Min();


                    }
                    else
                    {
                        inStock = true;
                    }

                    items.Add(new CarUserListItemViewModel
                    {
                        ID = car.ID,
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
                        CarTimePricing = (DB.GetList<CarTimePricing>().ToList()).SingleOrDefault(cp => cp.CarID == car.ID) ?? new CarTimePricing(),
                        CityID = (int)CityID,
                        IsInStock = inStock,
                        WillBeAviable = abilityTime,
                        OrdersCount = car.OrdersCount
                    });
                }

                switch (SortBy)
                {
                    case "Price Low":
                        {
                            items = items.OrderBy(vm => vm.CarTimePricing.PricePer1Day).ToList();
                        }
                        break;

                    case "Price High":
                        {
                            items = items.OrderByDescending(vm => vm.CarTimePricing.PricePer1Day).ToList();
                        }
                        break;

                    case "Name":
                        {
                            items = items.OrderBy(vm => vm.Brand).ToList();
                        }
                        break;

                    case "Popularity":
                        {
                            items = items.OrderByDescending(vm => vm.OrdersCount).ToList();
                        }
                        break;
                }

                if (SelectedGrades != null)
                {
                    var temp = new List<CarUserListItemViewModel>();
                    foreach (var item in items)
                    {
                        if (SelectedGrades.Contains(item.Grade))
                            temp.Add(item);
                    }
                    items = temp;
                }

                if (SelectedEngineTypes != null)
                {
                    var temp = new List<CarUserListItemViewModel>();
                    foreach (var item in items)
                    {
                        if (SelectedEngineTypes.Contains(item.EngineType))
                            temp.Add(item);
                    }
                    items = temp;
                }

                if (MinPricePerDay == null)
                {
                    var pricing = DB.GetList<CarTimePricing>().ToList();
                    MinPricePerDay = 0;
                    MaxPricePerDay = pricing.Max(p => p.PricePer1Day);
                }

                var temp1 = new List<CarUserListItemViewModel>();
                foreach (var item in items)
                {
                    if (item.CarTimePricing.PricePer1Day >= MinPricePerDay && item.CarTimePricing.PricePer1Day <= MaxPricePerDay)
                    {
                        temp1.Add(item);
                    }
                }
                items = temp1;
            }

            foreach (var item in items)
            {
                item.IconDescription = new Dictionary<string, string>();

                string path = Constants.IconsPath;
                switch (item.EngineType)
                {
                    case "Petrol":
                        {
                            item.IconDescription.Add(path + "gas.png", "Gas");
                        }
                        break;
                    case "Hybrid":
                        {
                            item.IconDescription.Add(path + "hybrid.png", "Hybrid");
                        }
                        break;
                    case "Electro":
                        {
                            item.IconDescription.Add(path + "electric.png", "Electro");
                        }
                        break;
                }

                switch (item.TransmissionType)
                {
                    case "Automatic":
                        {
                            item.IconDescription.Add(path + "automatic.png", "Automatic");
                        }
                        break;
                    case "SemiAutomatic":
                        {
                            item.IconDescription.Add(path + "automatic.png", "SemiAutomatic");
                        }
                        break;
                    case "Manual":
                        {
                            item.IconDescription.Add(path + "manual.png", "Manual");
                        }
                        break;
                }

                item.IconDescription.Add(path + "fuel.png", item.FuelConsumption + "L/100km");
                item.IconDescription.Add(path + "people.png", item.PassangerCount.ToString());
                item.IconDescription.Add(path + "door1.png", item.DoorCount.ToString());

                if (item.HasAirConditioning)
                    item.IconDescription.Add(path + "conditioner.png", "Has condition");
                else
                {
                    item.IconDescription.Add(path + "conditioner.png", "No condition");
                }
            }

            var finalList = new List<CarUserListItemViewModel>();
            foreach (var item in items)
            {
                var pricing = (DB.GetList<CarTimePricing>()).SingleOrDefault(p => p.CarID == item.ID);
                if (pricing != null)
                {
                    finalList.Add(item);
                }
            }

            vo.CarListDownloadedShown(finalList.Select(fl => fl.ID).ToArray());
            return PartialView(finalList);
        }

        [HttpGet]
        public ActionResult RentForm(string idUrl, string cityID)
        {
            var user = DB.GetEntityById<ApplicationUser>(User.Identity.GetUserId()) as ApplicationUser;
            if (!user.PhoneNumberConfirmed)
            {
                vo.UserIsNotLogined();

                return RedirectToAction("AddPhoneNumber", "Manage");
            }

            if(user.Fine > 0 || user.Debt > 0)
            {
                vo.UserHasDebt();

                int amount = user.Debt + user.Fine;
                return RedirectToAction("UserHasDebt", "Error", new { amount = amount });
            }

            try
            {
                int id = int.Parse(idUrl);
                var cityId = int.Parse(cityID);
                var car = DB.GetEntityById<Car>(id) as Car;
                var city = DB.GetEntityById<City>(cityId);

                if (car != null && city != null)
                {
                    var viewModel = new OrderCreateViewModel()
                    {
                        CarID = car.ID,
                        Cars = new SelectList(DB.GetList<Car>(), "ID", "BrandModel"),
                        OfficesStart = new SelectList(DB.GetList<Office>().Where(o => o.IsArchived == false && o.CityID == cityId), "ID", "PlaceDescription"),
                        Offices = new SelectList(DB.GetList<Office>().Where(o => o.IsArchived == false), "ID", "PlaceDescription"),
                        AdditionalOptions = DB.GetList<AdditionalOption>().ToList(),
                        CarTimePricing = DB.GetList<CarTimePricing>().SingleOrDefault(ctp => ctp.CarID == car.ID) as CarTimePricing ?? new CarTimePricing(),
                        RentStartDate = null,
                        Car = car
                    };

                    vo.FormSent();

                    return View(viewModel);
                }
                else
                {
                    vo.CarNotFound(car.ID.ToString());

                    return RedirectToAction("CarNotFound", "Error");
                }
            }
            catch (ArgumentException)
            {
                vo.WrongUrl(idUrl);

                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                vo.WrongUrl(idUrl);

                return RedirectToAction("WrongUrl", "Error");
            }
        }

        [HttpGet]
        public ActionResult GetForm(OrderCreateViewModel viewModel)
        {
            vo.FormDownloadedd();

            return PartialView("GetForm", viewModel);
        }

        [HttpPost]
        public ActionResult RentForm(OrderCreateViewModel viewModel)
        {
            var user = DB.GetEntityById<ApplicationUser>(User.Identity.GetUserId()) as ApplicationUser;
            if (user.Fine > 0 || user.Debt > 0)
            {
                vo.UserHasDebt();

                int amount = user.Debt + user.Fine;
                return RedirectToAction("UserHasDebt", "Error", new { amount = amount });
            }

            if (viewModel != null)
            {
                viewModel.UserID = User.Identity.GetUserId();

                viewModel.AdditionalOptions = new List<AdditionalOption>();
                var allOptions = DB.GetList<AdditionalOption>().ToList();
                for (int i = 0; i < allOptions.Count; i++)
                {
                    if (viewModel.OptionsSelected[i])
                    {
                        viewModel.AdditionalOptions.Add(allOptions[i]);
                    }
                }

                var pricing = (DB.GetList<CarTimePricing>()).SingleOrDefault(p => p.CarID == viewModel.CarID);
                int price = 0;
                int days = ((DateTime)viewModel.RentFinishDate - (DateTime)viewModel.RentStartDate).Days;

                var PricePer1Day = pricing.PricePer1Day;
                var PricePer3Days = pricing.PricePer3Days;
                var PricePer7Days = pricing.PricePer7Days;
                var PricePer14Days = pricing.PricePer14Days;
                var PricePerMonth = pricing.PricePerMonth;
                var PricePerMoreThanMonth = pricing.PricePerMoreThanMonth;

                if (days <= 1)
                {
                    price = 1 * PricePer1Day;
                }
                else if (days <= 3)
                {
                    price = days * PricePer3Days;
                }
                else if (days <= 7)
                {
                    price = days * PricePer7Days;
                }
                else if (days <= 14)
                {
                    price = days * PricePer14Days;
                }
                else if (days <= 30)
                {
                    price = days * PricePerMonth;
                }
                else
                {
                    price = days * PricePerMoreThanMonth;
                }

                foreach (var option in viewModel.AdditionalOptions)
                {
                    price += option.Price;
                }

                if((DateTime)viewModel.RentStartDate > DateTime.Now.AddDays(3))
                {
                    ModelState.AddModelError("RentStartDateTime", "THE DATE OF THE BEGINNING OF THE ORDER MUST NOT START LATER THAN IN 3 DAYS");
                }

                if (ModelState.IsValid)
                {
                    var order = new Order()
                    {
                        UserID = viewModel.UserID,
                        CarID = viewModel.CarID,
                        AdditionalOptionsJson = JsonConvert.SerializeObject(viewModel.AdditionalOptions),
                        RentStartDateTime = (DateTime)viewModel.RentStartDate,
                        RentFinishDateTime = (DateTime)viewModel.RentFinishDate,
                        OrderDateTime = DateTime.Now,
                        OfficeIdStart = viewModel.OfficeIdStart,
                        OfficeIdEnd = viewModel.OfficeIdEnd,
                        Comment = viewModel.Comment,
                        Price = price,
                        Status = "Waiting for manager review"
                    };

                    viewModel.OfficeStart = DB.GetEntityById<Office>(viewModel.OfficeIdStart) as Office;
                    var cityID = (DB.GetEntityById<Office>(viewModel.OfficeIdStart) as Office).CityID;
                   
                    var orders = DB.GetList<Order>().ToList();
                    foreach(var item in orders)
                    {
                        item.OfficeStart = DB.GetEntityById<Office>(item.OfficeIdStart) as Office;
                    }

                    orders = orders.Where(o => o.CarID == order.CarID && o.Stock.CityID == viewModel.OfficeStart.CityID)
                        .ToList();

                    var stocksFree = DB.GetList<Stock>().Where(s => s.CityID == cityID
                        && s.CarID == viewModel.CarID 
                        && s.IsBusy == false).ToList();
                
                    if (stocksFree.Count != 0)
                    {
                        order.StockID = stocksFree.First().ID;
                        stocksFree.First().IsBusy = true;
                        DB.Save<Order>(order);

                        user = DB.GetEntityById<ApplicationUser>(order.UserID) as ApplicationUser;
                        user.Debt += order.Price;

                        var car = DB.GetEntityById<Car>(order.CarID) as Car;
                        car.OrdersCount += 1;

                        DB.Update<Car>(order.CarID);

                        vo.OrderSaved();

                        DB.Update<ApplicationUser>(user.Id);
                    }
                    else
                    {
                        vo.NoFreeStoks();

                        return RedirectToAction("OrderNotFound", "Error");
                    }
                  
                    return JavaScript("window.location = 'http://localhost:55702/User/OrderSuccess'");
                }
                else
                {
                    viewModel.Cars = new SelectList(DB.GetList<Car>(), "ID", "BrandModel");
                    viewModel.Offices = new SelectList(DB.GetList<Office>(), "ID", "PlaceDescription");
                    viewModel.AdditionalOptions = DB.GetList<AdditionalOption>().ToList();
                    viewModel.CarTimePricing = DB.GetList<CarTimePricing>().SingleOrDefault(ctp => ctp.CarID == viewModel.CarID) as CarTimePricing;

                    vo.FormIsNotValid(ModelState.Values.SelectMany(v => v.Errors.Select(b => b.ErrorMessage)));

                    return View("GetForm", viewModel);
                }
            }
            else
            {
                vo.CarNotFound(viewModel.ID.ToString());

                return RedirectToAction("CarNotFound", "Error");
            }
        }

        public ActionResult OrderSuccess()
        {
            return View();
        }

        public ActionResult RentRules()
        {

            return View();
        }

        public ActionResult Contacts()
        {
            var offices = DB.GetList<Office>().Where(o => o.IsArchived == false).ToList();

            return View(offices); 
        }
    }

}