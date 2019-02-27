using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRent.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }
    }
}