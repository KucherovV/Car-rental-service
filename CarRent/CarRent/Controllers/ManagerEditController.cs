using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DataBase;
using Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using CarRent.ViewModels;

namespace CarRent.Controllers
{
    [Authorize(Roles ="admin")]
    public class ManagerEditController : Controller
    {
        private readonly DB DB;

        public ManagerEditController(DB Db)
        {
            DB = Db;
        }
        public ManagerEditController()
        {
            DB = new DB();
        }

        private ApplicationUserManager _userManager;

        //public ManagerEditController()
        //{
        //}

        public ManagerEditController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ActionResult Index()
        {
            var users = (DB.GetUsers() as IEnumerable<ApplicationUser>).ToList();
            var managers = new List<ManagerRegisterViewModel>();

            if (users == null)
                users = new List<ApplicationUser>();
          
            using(var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DB.GetContext())))
            {
                foreach(var user in users)
                {
                    if(userManager.GetRoles(user.Id).First() == "moderator")
                    {
                        managers.Add(new ManagerRegisterViewModel()
                        {
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            ManagerID = user.Id
                        });
                    }
                }
            }

            if(managers == null)
            {
                managers = new List<ManagerRegisterViewModel>();
            }

            return View(managers);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(ManagerRegisterViewModel viewModel)
        {
            var manager = new ApplicationUser()
            {
                Email = viewModel.Email,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName
            };

            if(manager != null && ModelState.IsValid)
            {
                manager.BirthDate = DateTime.Now.AddYears(-100);
                manager.DrivingLicenseDate = DateTime.Now;
                manager.UserName = viewModel.Email; 

                var test =  UserManager.Create(manager, viewModel.Password);
                var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(DB.GetContext()));

                userManager.AddToRole(manager.Id, "moderator");

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Email", "This email is already takken");
                return View(viewModel);
            }
        }

        public ActionResult Delete(string id)
        {
            var user = DB.GetEntityById<ApplicationUser>(id) as ApplicationUser;
            if (user != null)
            {
                var manager = new ManagerRegisterViewModel()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    ManagerID = user.Id
                };

                return View(manager);
            }
            else
            {
                return RedirectToAction("ManagerNotFound", "Error");
            }
        }

        [HttpGet]
        public ActionResult DeleteConfirmed(string id)
        {
            var user = DB.GetEntityById<ApplicationUser>(id) as ApplicationUser;
            if (user != null)
            {
                DB.Delete<ApplicationUser>(user);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("ManagerNotFound", "Error");
            }
            
        }
    } 
}




























