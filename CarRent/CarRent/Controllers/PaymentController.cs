using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using CarRent.ViewModels;
using DataBase;

namespace CarRent.Controllers
{
    public class PaymentController : Controller
    {
        public ActionResult Index()
        {
            var usersWithDebt = DB.GetList<ApplicationUser>().Where(u => u.Fine > 0 || u.Debt > 0).ToList();
            var listUsers = new List<PaymentIndex>();

            foreach (var user in usersWithDebt)
            {
                var viewModel = new PaymentIndex()
                {
                    Email = user.Email,
                    UserNamee = user.FirstName + " " + user.LastName,
                    UserID = user.Id
                };

                listUsers.Add(viewModel);
            }

            return View(listUsers);
        }

        [HttpGet]
        public ActionResult Edit(string userID)
        {
            var user = DB.GetEntityById<ApplicationUser>(userID) as ApplicationUser;

            if(user != null)
            {

                return View(user);
            }
            else
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        public ActionResult GetUserInfo(string userID)
        {
            var user = DB.GetEntityById<ApplicationUser>(userID) as ApplicationUser;

            if (user != null)
            {
                return PartialView(user);
            }
            else
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        public ActionResult PayDebt(string userID)
        {
            var user = DB.GetEntityById<ApplicationUser>(userID) as ApplicationUser;

            if (user != null)
            {
                user.Debt = 0;
                DB.Update<ApplicationUser>(user.Id);

                return PartialView("GetUserInfo", user);
            }
            else
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        public ActionResult PayFine(string userID)
        {
            var user = DB.GetEntityById<ApplicationUser>(userID) as ApplicationUser;

            if (user != null)
            {
                user.Fine = 0;
                DB.Update<ApplicationUser>(user.Id);

                return PartialView("GetUserInfo", user);
            }
            else
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }
    }
}