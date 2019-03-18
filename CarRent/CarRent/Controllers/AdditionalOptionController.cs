using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using Entities;

namespace CarRent.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdditionalOptionController : Controller
    {
        private readonly DB DB;

        public AdditionalOptionController(DB Db)
        {
            DB = Db;
        }
        public AdditionalOptionController()
        {
            DB = new DB();
        }

        public ActionResult Index()
        {
            var additionalOptions = DB.GetList<AdditionalOption>().ToList();
            if(additionalOptions == null)
            {
                additionalOptions = new List<AdditionalOption>();
            }

            return View(additionalOptions);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdditionalOption additionalOption)
        {
            if(additionalOption != null)
            {
                DB.Save<AdditionalOption>(additionalOption);

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
      
        [HttpGet]
        public ActionResult Delete(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var additionalOption = DB.GetEntityById<AdditionalOption>(id) as AdditionalOption;

                if (additionalOption != null)
                {
                    return View(additionalOption);
                }
                else
                {
                    return RedirectToAction("AdditionalOptionNotFound", "Error");
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

        public ActionResult DeleteConfirmed(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var additionalOption = DB.GetEntityById<AdditionalOption>(id) as AdditionalOption;
                if (additionalOption != null)
                {
                    DB.Delete<AdditionalOption>(additionalOption);

                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("AdditionalOptionNotFound", "Error");
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
