using System.Web.Mvc;

namespace CarRent.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult FileUploadLimitExceeded()
        {
            return View();
        }

        public ActionResult CarNotFound()
        {
            return View();
        }

        public ActionResult AdditionalOptionNotFound()
        {
            return View();
        }

        public ActionResult ManagerNotFound()
        {
            return View();
        }

        public ActionResult UserNotFound()
        {
            return View();
        }

        public ActionResult WrongUrl()
        {
            return View();
        }

        public ActionResult UnknownError()
        {
            return View();
        }
    }
}