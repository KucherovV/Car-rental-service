using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataBase;
using CarRent.ViewModels;
using System.IO;
using System.Web.Helpers;
using System.Collections.Generic;

namespace CarRent.Controllers
{
    [Authorize(Roles = "admin")]
    public class CarController : Controller
    {
        private readonly DB DB;

        public CarController(DB Db)
        {
            DB = Db;
        }
        public CarController()
        {
            DB = new DB();
        }

        public ActionResult Index(string search, bool? showArchive)
        {
            return View();
        }

        public ActionResult GetCarList(string search, bool? showArchive)
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

            var viewModels = new List<CarListAdminViewModel>();
            foreach (var car in cars)
            {
                var pricing = (DB.GetList<CarTimePricing>()).SingleOrDefault(p => p.CarID == car.ID);
                bool hasValue;
                if (pricing == null)
                    hasValue = false;
                else
                    hasValue = true;

                viewModels.Add(new CarListAdminViewModel()
                {
                    Car = car,
                    HasPricing = hasValue
                });

            }
            
            return PartialView(viewModels);
        }


        [HttpGet]
        public ActionResult Create()
        {
            CarCreateViewModel carCreateViewModel = new CarCreateViewModel()
            {
                BrandsList = new SelectList(Enumerations.Brands.OrderBy(b =>b)),
                EngineTypes = new SelectList(Enumerations.EngineTypes),
                TransmissionTypes = new SelectList(Enumerations.TransmissionTypes),
                GradeList = new SelectList(Enumerations.Grades)
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
                Car car = new Car()
                {
                    Brand = carCreateViewModel.Brand,
                    Grade = carCreateViewModel.SelectedGrade,
                    DoorCount = carCreateViewModel.DoorCount,
                    EngineType = carCreateViewModel.SelectedEngineType,
                    FuelConsumption = carCreateViewModel.FuelConsumption,
                    HasAirConditioning = carCreateViewModel.HasAirConditioning,
                    LuggageCount = carCreateViewModel.LuggageCount,
                    Model = carCreateViewModel.Model,
                    PassangerCount = carCreateViewModel.PassangerCount,
                    TransmissionType = carCreateViewModel.SelectedTransmissionType,
                    FileName = file.FileName,
                    BrandModel = carCreateViewModel.Brand + " " +  carCreateViewModel.Model
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
        public ActionResult Edit(string idUrl)
        {        
            try
            {
                int id = int.Parse(idUrl);

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
                        GradeList = new SelectList(Enumerations.Grades),
                        SelectedGrade = car.Grade,
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
                    return RedirectToAction("CarNotFound", "Error");
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
                Car car = DB.GetEntityById<Car>(carCreateViewModel.CarID) as Car;
                car.Brand = carCreateViewModel.Brand;
                car.Grade = carCreateViewModel.SelectedGrade;
                car.DoorCount = carCreateViewModel.DoorCount;
                car.EngineType = carCreateViewModel.SelectedEngineType;
                car.FuelConsumption = carCreateViewModel.FuelConsumption;
                car.HasAirConditioning = carCreateViewModel.HasAirConditioning;
                car.LuggageCount = carCreateViewModel.LuggageCount;
                car.Model = carCreateViewModel.Model;
                car.PassangerCount = carCreateViewModel.PassangerCount;
                car.TransmissionType = carCreateViewModel.SelectedTransmissionType;
                car.BrandModel = carCreateViewModel.Brand + " " + carCreateViewModel.Model;

                if (file != null)
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

        //return view with archive confirmation
        [HttpGet]
        public ActionResult Archive(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var car = DB.GetEntityById<Car>(id) as Car;
                if (car != null)
                {
                    if (!car.Archived)
                    {
                        return View(car);
                    }
                    else
                    {
                        car.Archived = false;
                        DB.Update<Car>(car.ID);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("CarNotFound", "Error");
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

        //archive car
        [HttpGet]
        public ActionResult ArchiveConfirmed(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var car = DB.GetEntityById<Car>(id) as Car;
                if (car != null)
                {
                    car.Archived = true;
                    DB.Update<Car>(car.ID);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("CarNotFound", "Error");
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

        //validate file
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

        //save file
        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            WebImage img = new WebImage(file.InputStream);
            if (img.Width > 600)
            {
                img.Resize(600, img.Height);
            }
            img.Save(Constants.ProductImagePath + Path.GetFileName(file.FileName));         
        }      

        //return view with pricing managing
        [HttpGet]
        public ActionResult ManagePricing(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var car = DB.GetEntityById<Car>(id) as Car;              
                if (car != null)
                {
                    var carPricing = (DB.GetList<CarTimePricing>().ToList()).SingleOrDefault(cp => cp.CarID == car.ID) ?? new CarTimePricing();
                    var carPricingViewModel = new CarPricingViewModel()
                    {
                        CarID = car.ID,
                        CarName = car.Brand + " " + car.Model,
                        PricePer1Day = carPricing.PricePer1Day,
                        PricePer3Days = carPricing.PricePer3Days,
                        PricePer7Days = carPricing.PricePer7Days,
                        PricePer14Days = carPricing.PricePer14Days,
                        PricePerMonth = carPricing.PricePerMonth,
                        PricePerMoreThanMonth = carPricing.PricePerMoreThanMonth
                    };

                    return View(carPricingViewModel);
                }
                else
                {
                    return RedirectToAction("CarNotFound", "Error");
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

        //save pricing
        [HttpPost]
        public ActionResult ManagePricing(CarPricingViewModel carPricingViewModel)
        {
            if(carPricingViewModel != null)
            {
                bool isInDB = false;

                var carPricing = (DB.GetList<CarTimePricing>().ToList())
                    .SingleOrDefault(cp => cp.CarID == carPricingViewModel.CarID);
               
                if (carPricing == null)
                {
                    carPricing = new CarTimePricing();
                    isInDB = true;
                }

                carPricing.CarID = carPricingViewModel.CarID;
                carPricing.PricePer1Day = carPricingViewModel.PricePer1Day;
                carPricing.PricePer3Days = carPricingViewModel.PricePer3Days;
                carPricing.PricePer7Days = carPricingViewModel.PricePer7Days;
                carPricing.PricePer14Days = carPricingViewModel.PricePer14Days;
                carPricing.PricePerMonth = carPricingViewModel.PricePerMonth;
                carPricing.PricePerMoreThanMonth = carPricingViewModel.PricePerMoreThanMonth;

                if (isInDB)
                {
                    DB.Save<CarTimePricing>(carPricing);
                }
                else
                {
                    DB.Update<CarTimePricing>(carPricing.ID);
                }

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("UnknownError", "Error");
            }
        }
    }
}
