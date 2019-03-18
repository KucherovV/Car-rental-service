using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataBase;
using Entities;

namespace CarRent.Controllers
{
    [Authorize(Roles = "admin")]
    public class CityController : Controller
    {
        private readonly DB DB;

        public CityController(DB Db)
        {
            DB = Db;
        }
        public CityController()
        {
            DB = new DB();
        }

        public ActionResult Index()
        {
            var cities = DB.GetList<City>().ToList();

            if (cities == null)
            {
                cities = new List<City>();
            }

            return View(cities);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(City city)
        {
            if (city == null)
                return HttpNotFound();

            DB.Save<City>(city);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var city = DB.GetEntityById<City>(id) as City;
                DB.Delete<City>(city);

                return RedirectToAction("Index");
            }
            catch (ArgumentException)
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }
    }
}
