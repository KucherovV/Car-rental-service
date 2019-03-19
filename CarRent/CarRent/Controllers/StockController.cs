using System.Web.Mvc;
using DataBase;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using CarRent.ViewModels;
using System.Data.Entity.Infrastructure;

namespace CarRent.Controllers
{
    [Authorize(Roles = "admin")]
    public class StockController : Controller
    {
        private readonly DB DB;

        public StockController(DB Db)
        {
            DB = Db;
        }
        public StockController()
        {
            DB = new DB();
        }

        //get list of cities and stocks
        public ActionResult CitiesStock()
        {
            var cities = DB.GetList<City>().ToList();

            return View(cities);
        }

        public ActionResult Edit(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var city = DB.GetEntityById<City>(id) as City;
                if(city != null)
                {
                    return View(city);
                }
                else
                {
                    return RedirectToAction("WrongUrl", "Error");
                }
            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        //get list of cars in city
        public ActionResult GetStockList(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var city = DB.GetEntityById<City>(id) as City;
                var carInStocks = DB.GetList<Stock>().Where(cs => cs.CityID == city.ID).ToList();
                var carInStockCarIDs = carInStocks.Select(c => c.CarID).ToList();
                var cars = DB.GetList<Car>().Where(c => c.Archived != true).ToList();
                var stocks = DB.GetList<Stock>().ToList();

                var list = new List<CarInStockListViewModel>();

                foreach(var car in cars)
                {
                    list.Add(new CarInStockListViewModel()
                    {
                        Car = car,
                        Amount = stocks.Where(s => s.CarID == car.ID && s.CityID == city.ID).Count(),
                        AmountRented = stocks.Where(s => s.CarID == car.ID && s.CityID == city.ID && s.IsBusy == true).Count(),
                    });
                }
             
                var vm = new StockManageViewModel()
                {
                    CarsInStock = list,
                    City = city
                };

                return PartialView(vm);
            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        //get list of specific cars in city
        public ActionResult ListOfSpecificCars(string carID, string cityID)
        {
            try
            {
                int carId = int.Parse(carID);
                int cityId = int.Parse(cityID);

                var car = DB.GetEntityById<Car>(carId) as Car;
                var city = DB.GetEntityById<City>(cityId) as City;

                if (car != null && city != null)
                {
                    var stocks = DB.GetList<Stock>().Where(s => s.CityID == cityId && s.CarID == carId).ToList();

                    var viewModel = new SpecificCarListViewModel()
                    {
                        Car = car,
                        City = city
                    };

                    return View(viewModel);                   
                }
                else
                {
                    return RedirectToAction("WrongUrl", "Error");
                }
            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        public ActionResult GetListOfSpecificCars(string carID, string cityID)
        {
            try
            {
                int carId = int.Parse(carID);
                int cityId = int.Parse(cityID);

                var car = DB.GetEntityById<Car>(carId) as Car;
                var city = DB.GetEntityById<City>(cityId) as City;

                if (car != null && city != null)
                {
                    var stocks = DB.GetList<Stock>().Where(s => s.CityID == cityId && s.CarID == carId).ToList();

                    
                    return PartialView(stocks);
                }
                else
                {
                    return RedirectToAction("WrongUrl", "Error");
                }
            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        [HttpGet]
        public ActionResult AddCar(string cityID, string carID)
        {
            try
            {
                int carId = int.Parse(carID);
                int cityId = int.Parse(cityID);

                var car = DB.GetEntityById<Car>(carId) as Car;
                var city = DB.GetEntityById<City>(cityId) as City;

                if (car != null && city != null)
                {
                    var stock = new SpecificCarEditViewModel()
                    {
                        CarID = carId,
                        Car = car,
                        CityID = cityId,
                        City = city
                    };

                    ManagerController uc = new ManagerController();
                    uc.NoticeSubscribers(carId, cityId);

                    return View(stock);
                }
                else
                {
                    return RedirectToAction("WrongUrl", "Error");
                }

            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        [HttpGet]
        public ActionResult EditCar(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);
                var stock = DB.GetEntityById<Stock>(id) as Stock;

                if (stock != null && !stock.IsBusy)
                {
                    var viewModel = new SpecificCarEditViewModel()
                    {
                        Car = stock.Car,
                        CarID = stock.CarID,
                        City = stock.City,
                        CityID = stock.CityID,
                        Color = stock.Color,
                        Defects = stock.Defects,
                        ID = stock.ID,
                        RegistrationNumber = stock.RegistrationNumber,
                        VIN = stock.VIN,
                        IsEditing = true
                    };

                    return View("AddCar", viewModel);
                }
                else
                {
                    return RedirectToAction("WrongUrl", "Error");
                }

            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        [HttpPost]
        public ActionResult AddCar(SpecificCarEditViewModel viewModel)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var stock = new Stock();

                    if (viewModel.IsEditing)
                    {
                        stock = DB.GetEntityById<Stock>(viewModel.ID) as Stock;
                        stock.CarID = viewModel.CarID;
                        stock.CityID = viewModel.CityID;
                        stock.Color = viewModel.Color;
                        stock.Defects = viewModel.Defects;
                        stock.RegistrationNumber = viewModel.RegistrationNumber;
                        stock.VIN = viewModel.VIN;

                        DB.Update<Stock>(viewModel.ID);
                    }
                    else
                    {
                        stock = new Stock()
                        {
                            CarID = viewModel.CarID,
                            CityID = viewModel.CityID,
                            Color = viewModel.Color,
                            Defects = viewModel.Defects,
                            RegistrationNumber = viewModel.RegistrationNumber,
                            VIN = viewModel.VIN
                        };

                        DB.Save<Stock>(stock);
                    }

                    var managerController = new ManagerController();
                    managerController.NoticeSubscribers(viewModel.CarID, viewModel.CityID);

                    return RedirectToAction("ListOfSpecificCars", "Stock", new { cityID = viewModel.CityID, carID = viewModel.CarID });
                }
                else
                {
                    ModelState.AddModelError("VIN", "Vin is not unique");
                    viewModel.Car = DB.GetEntityById<Car>(viewModel.CarID) as Car;
                    viewModel.City = DB.GetEntityById<City>(viewModel.CityID) as City;

                    return View(viewModel);
                }
                
            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("VIN", "VIN is not unique");
                //return RedirectToAction("AddCar", "Stock", new { cityID = viewModel.CityID, carID = viewModel.CarID });
                return RedirectToAction("GetListOfSpecificCars", "Stock", new { cityID = viewModel.CityID, carID = viewModel.CarID });
            }
        }

        [HttpGet]
        public ActionResult Archive(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);
                var stock = DB.GetEntityById<Stock>(id) as Stock;

                if (stock != null && !stock.IsBusy)
                {
                    if (stock.IsArchive)
                    {
                        stock.IsArchive = false;
                    }
                    else
                    {
                        stock.IsArchive = true;
                    }

                    DB.Update<Stock>(stock.ID);

                    var stocks = DB.GetList<Stock>().Where(s => s.CityID == stock.CityID && s.CarID == stock.CarID).ToList();
                    return PartialView("GetListOfSpecificCars", stocks);
                }
                else
                {
                    return RedirectToAction("WrongUrl", "Error");
                }

            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }
    }
}