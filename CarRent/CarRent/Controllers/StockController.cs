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
                var list = new List<Stock>();

                foreach (var car in cars)
                {
                    if (!carInStockCarIDs.Contains(car.ID))
                    {
                        list.Add(new Stock()
                        {
                            Amount = 0,
                            CarID = car.ID,
                            CityID = city.ID,
                            Car = car
                        });
                    }
                    else
                    {
                        list.Add(carInStocks.Single(cs => cs.CarID == car.ID));
                    }
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

        public ActionResult AddCar(string idUrl, string cityID, string carID)
        {
            try
            {
                int id = int.Parse(idUrl);
                int cityId = int.Parse(cityID);
                int carId = int.Parse(carID);
                var car = DB.GetEntityById<Car>(carId) as Car;
                if(car == null)
                {
                    throw new ArgumentException();
                }

                if (id == 0)
                {
                    var carInstock = new Stock()
                    {
                        Amount = 1,
                        CarID = car.ID,
                        CityID = cityId
                    };
                    DB.Save<Stock>(carInstock);

                    return RedirectToAction("GetStockList", new { idUrl = cityID });
                }
                else
                {
                    var carInSrock = DB.GetEntityById<Stock>(id) as Stock;
                    if (carInSrock != null)
                    {
                        carInSrock.Amount++;
                        DB.Update<Stock>(id);

                        return RedirectToAction("GetStockList", new { idUrl = cityID });
                    }
                    else
                    {
                        return RedirectToAction("WrongUrl", "Error");
                    }
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

        public ActionResult RemoveCar(string idUrl, string cityID)
        {
            try
            {
                int id = int.Parse(idUrl);
                int cityId = int.Parse(cityID);

                if (id == 0)
                {
                    throw new ArgumentException();
                }
                else
                {
                    var carInSrock = DB.GetEntityById<Stock>(id) as Stock;
                    if (carInSrock != null)
                    {
                        carInSrock.Amount--;
                        DB.Update<Stock>(id);

                        return RedirectToAction("GetStockList", new { idUrl = cityID });
                    }
                    else
                    {
                        return RedirectToAction("GetStockList", new { idUrl = cityID });
                    }
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