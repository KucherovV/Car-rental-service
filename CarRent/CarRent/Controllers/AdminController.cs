using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataBase;
using Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using CarRent.ViewModels;

namespace CarRent.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationUserManager _userManager;

        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult UserList(bool? showBlock, string search)
        {
            var users = (DB.GetUsers() as IEnumerable<ApplicationUser>).ToList();
            var usersOnly = new List<UserIndexViewModel>();
            if (users == null)
                users = new List<ApplicationUser>();

            using (var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DB.GetContext())))
            {
                foreach (var user in users)
                {
                    if (userManager.GetRoles(user.Id).First() == "user")
                    {
                        usersOnly.Add(new UserIndexViewModel()
                        {
                            UserID = user.Id,
                            BirthDate = user.BirthDate.ToShortDateString(),
                            DrivingLicenseDate = user.DrivingLicenseDate.ToShortDateString(),
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            IDNumber = user.IDNumber,
                            PhoneNumber = !String.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : "none",
                            IsBlocked = user.IsBlocked
                        });
                    }
                }
            }

            if (showBlock == true)
            {
                usersOnly = usersOnly.OrderBy(u => u.IsBlocked).ToList();
            }
            else if (showBlock == null || showBlock == false)
            {
                usersOnly = usersOnly.Where(u => u.IsBlocked == false).ToList();
            }

            if (!String.IsNullOrEmpty(search))
            {
                usersOnly = usersOnly.Where(u => u.FirstName.Contains(search)
                || u.LastName.Contains(search)
                || u.Email.Contains(search)).ToList();
            }


            return View(usersOnly);
        }

        public ActionResult Details(string id)
        {
            var user = DB.GetEntityById<ApplicationUser>(id) as ApplicationUser;
            if (user != null)
            {
                var userViewModel = new UserDetailsViewModel()
                {
                    UserID = user.Id,
                    BirthDate = user.BirthDate.ToShortDateString(),
                    DrivingLicenseDate = user.DrivingLicenseDate.ToShortDateString(),
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    IDNumber = user.IDNumber,
                    PhoneNumber = !String.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : "none",
                    IsBlocked = user.IsBlocked
                };

                return View(userViewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult BlockUser(Blocking blocking, string userID)
        {
            if (userID != null)
            {
                var user = DB.GetEntityById<ApplicationUser>(userID) as ApplicationUser;

                if (blocking.BlockFinish < DateTime.Now)
                {
                    ModelState.AddModelError("BlockFinish", "Block finish cannot be in past");

                    var userViewModel = new UserDetailsViewModel()
                    {
                        UserID = user.Id,
                        BirthDate = user.BirthDate.ToShortDateString(),
                        DrivingLicenseDate = user.DrivingLicenseDate.ToShortDateString(),
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        IDNumber = user.IDNumber,
                        PhoneNumber = !String.IsNullOrEmpty(user.PhoneNumber) ? user.PhoneNumber : "none"
                    };

                    return View("Details", userViewModel);
                }

                blocking.BlockStart = DateTime.Now;
                blocking.UserID = userID;

                user.IsBlocked = true;
                user.BlockEnd = blocking.BlockFinish;

                DB.Update<ApplicationUser>(userID);
                DB.Save<Blocking>(blocking);

                return RedirectToAction("UserList");
            }

            return HttpNotFound();

        }

        public ActionResult UnblockUser(string userID)
        {
            if (userID != null)
            {
                var user = DB.GetEntityById<ApplicationUser>(userID) as ApplicationUser;

                user.IsBlocked = false;

                DB.Update<ApplicationUser>(userID);

                var blockings = (DB.GetList<Blocking>() as IEnumerable<Blocking>).ToList();
                var blockingItem = blockings.SingleOrDefault(b => b.UserID == userID);
                DB.Delete<Blocking>(blockingItem);

                return RedirectToAction("UserList");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}