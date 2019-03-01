using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using Entities;

namespace CarRent.Controllers
{
    public class AdditionalOptionController : Controller
    {
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
        public ActionResult Delete(int id)
        {
            var additionalOption = DB.GetEntityById<AdditionalOption>(id) as AdditionalOption;
            if(additionalOption != null)
            {
                return View(additionalOption);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult DeleteConfirmed(int id)
        {
            var additionalOption = DB.GetEntityById<AdditionalOption>(id) as AdditionalOption;
            if (additionalOption != null)
            {
                DB.Delete<AdditionalOption>(additionalOption);

                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}
