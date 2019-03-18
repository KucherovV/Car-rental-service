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
    public class OfficeController : Controller
    { 
        private readonly DB DB;

        public OfficeController(DB Db)
        {
            DB = Db;
        }
        public OfficeController()
        {
            DB = new DB();
        }

        public ActionResult Index()
        {
            var cities = DB.GetList<City>().ToList();
            var offices = DB.GetList<Office>().OrderBy(o => o.IsArchived).ToList();
            var viewModel = new List<OfficeIndexViewModel>() { };

            foreach(var city in cities)
            {
                viewModel.Add(new OfficeIndexViewModel()
                {
                    Amount = offices.Where(o => o.CityID == city.ID).Count(),
                    City = city
                });
            }
         
            return View(viewModel);
        }

        public ActionResult ViewOffices(string idUrl)
        {
            try
            {
                int cityID = int.Parse(idUrl);
                var city = DB.GetEntityById<City>(cityID) as City;

                if(city != null)
                {
                    var offices = DB.GetList<Office>()
                        .Where(o => o.CityID == cityID)
                        .ToList();

                    if(offices == null)
                    {
                        offices = new List<Office>();
                    }

                    var viewModel = new OfficeInCityCiewModel()
                    {
                        Offices = offices,
                        City = city
                    };

                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("CityNotFound", "Error");
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
        public ActionResult Create(string idUrl)
        {
            try
            {
                int cityID = int.Parse(idUrl);
                var city = DB.GetEntityById<City>(cityID) as City;

                if (city != null)
                {
                    var office = new Office()
                    {
                        CityID = cityID
                    };

                    return View(office);
                }
                else
                {
                    return RedirectToAction("CityNotFound", "Error");
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
        public ActionResult Create(Office office)
        {
            if(office != null)
            {
                DB.Save<Office>(office);

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("CityNotFound", "Error");
            }

        }

        [HttpGet]
        public ActionResult Edit(string idUrl)
        {
            try
            {
                int officeID = int.Parse(idUrl);
                var office = DB.GetEntityById<Office>(officeID) as Office;

                if (office != null)
                {                
                    return View(office);
                }
                else
                {
                    return RedirectToAction("OfficeNotFound", "Error");
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
        public ActionResult Edit(Office office)
        {
            if(office != null)
            {
                var officeToUpdate = DB.GetEntityById<Office>(office.ID) as Office;
                officeToUpdate.PhoneNumber = office.PhoneNumber;
                officeToUpdate.PlaceDescription = office.PlaceDescription;
                officeToUpdate.Address = office.Address;

                DB.Update<Office>(officeToUpdate.ID);

                //return RedirectToAction("Index");
                return RedirectToAction("ViewOffices", new { idUrl = office.CityID.ToString() });
            }
            else
            {
                return RedirectToAction("OfficeNotFound", "Error");
            }
        }

        public ActionResult Archive(string idUrl)
        {
            try
            {
                int officeID = int.Parse(idUrl);
                var office = DB.GetEntityById<Office>(officeID) as Office;

                if (office != null)
                {
                    if (office.IsArchived)
                    {
                        office.IsArchived = false;
                    }
                    else
                    {
                        office.IsArchived = true;
                    }
                    DB.Update<Office>(office.ID);

                    //return RedirectToAction("Index");
                    return RedirectToAction("ViewOffices", new { idUrl = office.CityID.ToString() });
                }
                else
                {
                    return RedirectToAction("OfficeNotFound", "Error");
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