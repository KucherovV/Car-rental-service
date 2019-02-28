using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataBase;
using CarRent.ViewModels;
using System.IO;
using System.Web.Helpers;

namespace CarRent.Controllers
{
    [Authorize(Roles = "admin")]
    public class CarController : Controller
    {
        public ActionResult Index(string search, bool? showArchive)
        {
            var cars = DB.GetList<Car>();

            if (showArchive == true)
            {
                cars = cars.OrderBy(c => c.Archived).ToList();
            }
            else if (showArchive == null || showArchive == false)
            {
                cars = cars.Where(c => c.Archived == false).ToList();
            }

            if (!String.IsNullOrEmpty(search))
            {
                cars = cars.Where(c => c.Brand.Contains(search) || c.Model.Contains(search)).ToList();
            }

            return View(cars);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            CarCreateViewModel carCreateViewModel = new CarCreateViewModel()
            {
                BrandsList = new SelectList(Enumerations.Brands.OrderBy(b =>b)),
                EngineTypes = new SelectList(Enumerations.EngineTypes),
                TransmissionTypes = new SelectList(Enumerations.TransmissionTypes),
            };

            return View(carCreateViewModel);
        }

        [HttpPost]
        public ActionResult Create(CarCreateViewModel carCreateViewModel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (ValidateFile(file))
                {
                    try
                    {
                        SaveFileToDisk(file);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("FileName", "An error occured, try again");
                    }
                }
                else
                {
                    ModelState.AddModelError("FileName", "The file must be gif, png, jpeg or jpg amd less than 2MB");
                }
            }
            else
            {
                ModelState.AddModelError("FileName", "Please choose a file");
            }

            if (ModelState.IsValid)
            {
                //string brand = DB.GetEntityById<Brand>(carCreateViewModel.BrandID).ToString();

                Car car = new Car()
                {
                    Brand = carCreateViewModel.Brand,
                    DoorCount = carCreateViewModel.DoorCount,
                    EngineType = carCreateViewModel.SelectedEngineType,
                    FuelConsumption = carCreateViewModel.FuelConsumption,
                    HasAirConditioning = carCreateViewModel.HasAirConditioning,
                    LuggageCount = carCreateViewModel.LuggageCount,
                    Model = carCreateViewModel.Model,
                    PassangerCount = carCreateViewModel.PassangerCount,
                    TransmissionType = carCreateViewModel.SelectedTransmissionType,
                    FileName = file.FileName
                };

                DB.Save<Car>(car);

                return RedirectToAction("Index");
            }
            else
            {
                carCreateViewModel.BrandsList = new SelectList(DB.GetList<Brand>().ToList(), "ID", "Name");
                return View(carCreateViewModel);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var car = DB.GetEntityById<Car>(id) as Car;

            if (car != null)
            {
                CarCreateViewModel carCreateViewModel = new CarCreateViewModel()
                {
                    CarID = id,
                    Brand = car.Brand,
                    BrandsList = new SelectList(Enumerations.Brands.OrderBy(b => b)),
                    TransmissionTypes = new SelectList(Enumerations.TransmissionTypes),
                    EngineTypes = new SelectList(Enumerations.EngineTypes),
                    DoorCount = car.DoorCount,
                    FileName = car.FileName,
                    FuelConsumption = car.FuelConsumption,
                    HasAirConditioning = car.HasAirConditioning,
                    LuggageCount = car.LuggageCount,
                    Model = car.Model,
                    PassangerCount = car.PassangerCount,
                    SelectedEngineType = car.EngineType,
                    SelectedTransmissionType = car.TransmissionType
                };

                return View(carCreateViewModel);
            }
            else
            {
                return HttpNotFound();
            }        
        }

        [HttpPost]
        public ActionResult Edit(CarCreateViewModel carCreateViewModel, HttpPostedFileBase file)
        {
            if (file != null)
            {
                if (ValidateFile(file))
                {
                    try
                    {
                        SaveFileToDisk(file);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("FileName", "An error occured, try again");
                    }
                }
                else
                {
                    ModelState.AddModelError("FileName", "The file must be gif, png, jpeg or jpg amd less than 2MB");
                }
            }            
            if (ModelState.IsValid)
            {
                //string brand = DB.GetEntityById<Brand>(carCreateViewModel.BrandID).ToString();

                Car car = DB.GetEntityById<Car>(carCreateViewModel.CarID) as Car;
                car.Brand = carCreateViewModel.Brand;
                car.DoorCount = carCreateViewModel.DoorCount;
                car.EngineType = carCreateViewModel.SelectedEngineType;
                car.FuelConsumption = carCreateViewModel.FuelConsumption;
                car.HasAirConditioning = carCreateViewModel.HasAirConditioning;
                car.LuggageCount = carCreateViewModel.LuggageCount;
                car.Model = carCreateViewModel.Model;
                car.PassangerCount = carCreateViewModel.PassangerCount;
                car.TransmissionType = carCreateViewModel.SelectedTransmissionType;

                if(file != null)
                    car.FileName = file.FileName;
               
                DB.Update<Car>(car.ID);

                return RedirectToAction("Index");
            }
            else
            {
                carCreateViewModel.BrandsList = new SelectList(DB.GetList<Brand>().ToList(), "ID", "Name");
                return View(carCreateViewModel);
            }
        }
        
        [HttpGet]
        public ActionResult Archive(int id)
        {
            var car = DB.GetEntityById<Car>(id) as Car;
            if (!car.Archived)
            {
                if (car != null)
                {
                    return View(car);
                }
            }
            else
            {
                car.Archived = false;
                DB.Update<Car>(car.ID);
                return RedirectToAction("Index");
            }

            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult ArchiveConfirmed(int id)
        {
            var car = DB.GetEntityById<Car>(id) as Car;
            if(car != null)
            {
                car.Archived = true;
                DB.Update<Car>(car.ID);
                return RedirectToAction("Index");
            }
            else
            {
                return HttpNotFound();
            }
        }    

        private bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtensions = Path.GetExtension(file.FileName).ToLower();
            string[] allowedTypes = { ".gif", ".png", ".jpeg", ".jpg" };
            if ((file.ContentLength > 0 && file.ContentLength < 2097152) &&
                allowedTypes.Contains(fileExtensions))
            {
                return true;
            }
            return false;
        }

        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width > 600)
            {
                img.Resize(600, img.Height);
            }
            img.Save(Constants.ProductImagePath + Path.GetFileName(file.FileName));         
        }      
    }
}
