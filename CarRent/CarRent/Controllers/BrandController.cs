using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataBase;
using Entities;

namespace CarRent.Controllers
{
    [Authorize(Roles = "admin")]
    public class BrandController : Controller
    {
        public ActionResult Index()
        {
            var brands = DB.GetList<Brand>().ToList();
            
            if(brands == null)
            {
                brands = new List<Brand>();
            }

            return View(brands);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
      
        [HttpPost]
        public ActionResult Create(Brand brand)
        {
            if (brand == null)
                return HttpNotFound();

            DB.Save<Brand>(brand);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var brand = DB.GetEntityById<Brand>(id) as Brand;
            DB.Delete<Brand>(brand);

            return RedirectToAction("Index");
        }
    }
}
