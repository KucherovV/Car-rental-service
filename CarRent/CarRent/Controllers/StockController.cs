using System.Web.Mvc;
using DataBase;
using Entities;
using System.Collections.Generic;
using System.Linq;
using System;
using CarRent.ViewModels;

namespace CarRent.Controllers
{
    [Authorize(Roles = "admin")]
    public class StockController : Controller
    {
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
                        AmountRented = stocks.Where(s => s.CarID == car.ID && s.CityID == city.ID && s.RentStartDateTime != null).Count()
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

        public ActionResult AddCar(string cityID, string carID)
        {
            try
            {
                int cityId = int.Parse(cityID);
                int carId = int.Parse(carID);

                var city = DB.GetEntityById<City>(cityId) as City;
                var car = DB.GetEntityById<Car>(carId) as Car;

                if(city != null && car != null)
                {
                    var stock = new Stock()
                    {
                        CarID = carId,
                        CityID = cityId
                    };

                    DB.Save<Stock>(stock);
                    return RedirectToAction("GetStockList", new { idUrl = cityID });
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

        public ActionResult RemoveCar(string cityID, string carID)
        {
            try
            {
                int cityId = int.Parse(cityID);
                int carId = int.Parse(carID);

                var city = DB.GetEntityById<City>(cityId) as City;
                var car = DB.GetEntityById<Car>(carId) as Car;

                if (city != null && car != null)
                {
                    var stock = DB.GetList<Stock>().First(s => s.CarID == carId && s.CityID == cityId && s.RentStartDateTime == null);

                    DB.Delete<Stock>(stock);

                    return RedirectToAction("GetStockList", new { idUrl = cityID });
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

            //    try
            //    {
            //        int id = int.Parse(idUrl);
            //        int cityId = int.Parse(cityID);

            //        if (id == 0)
            //        {
            //            throw new ArgumentException();
            //        }
            //        else
            //        {
            //            var carInSrock = DB.GetEntityById<Stock>(id) as Stock;
            //            if (carInSrock != null)
            //            {
            //                carInSrock.Amount--;
            //                DB.Update<Stock>(id);

            //                return RedirectToAction("GetStockList", new { idUrl = cityID });
            //            }
            //            else
            //            {
            //                return RedirectToAction("GetStockList", new { idUrl = cityID });
            //            }
            //        }
            //    }
            //    catch (ArgumentException)
            //    {
            //        return RedirectToAction("WrongUrl", "Error");
            //    }
            //    catch (FormatException)
            //    {
            //        return RedirectToAction("WrongUrl", "Error");
            //    }
            //}
            return null;
        }
    }
}