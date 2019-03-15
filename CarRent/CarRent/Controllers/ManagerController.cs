using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataBase;
using Newtonsoft.Json;
using CarRent.ViewModels;

namespace CarRent.Controllers
{
    public class ManagerController : Controller
    {
        public ActionResult Orders()
        {
            var viewModel = new OrderListViewModel()
            {
                Statuses = new MultiSelectList(Enumerations.Statuses)
            };

            return View(viewModel);
        }

        public ActionResult GetOrdersList(string search, List<string> SelectedStatuses)
        {
            var orders = DB.GetList<Order>().ToList();
            var offices = DB.GetList<Office>().ToList();
            var viewModels = new List<ManagerOrderViewModel>();

            if (!String.IsNullOrEmpty(search))
            {
                orders = orders.Where(o => o.User.FirstName.ToLower().Contains(search) 
                || o.User.LastName.ToLower().Contains(search) 
                || o.ID.ToString().Contains(search)).ToList();
            }

            if (SelectedStatuses != null)
            {
                var temp = new List<Order>();
                foreach (var order in orders)
                {
                    if (SelectedStatuses.Contains(order.Status))
                        temp.Add(order);
                }
                orders = temp;
            }

            foreach(var order in orders)
            {
                string color = "";
                switch (order.Status)
                {
                    case "Waiting for manager review":
                        {
                            color = "red";
                        }
                        break;

                    case "Waiting for customer confirm":
                        {
                            color = "blue";
                        }
                        break;

                    case "Waiting for execution":
                        {
                            color = "green";
                        }
                        break;
                    case "Denied":
                        {
                            color = "gray";
                        }
                        break;
                }

                viewModels.Add(new ManagerOrderViewModel()
                {
                    ID = order.ID,
                    User = order.User,
                    Car = order.Car,
                    AdditionalOptions = JsonConvert.DeserializeObject<List<AdditionalOption>>(order.AdditionalOptionsJson),
                    OrderDateTime = order.OrderDateTime,
                    RentStartDateTime = order.RentStartDateTime,
                    RentFinishDateTime = order.RentFinishDateTime,
                    OfficeStart = offices.SingleOrDefault(o => o.ID == order.OfficeIdStart),
                    OfficeEnd = offices.SingleOrDefault(o => o.ID == order.OfficeIdEnd),
                    Comment = order.Comment,
                    Price = order.Price,
                    Status = order.Status,
                    Color = color
                });
            }

            return PartialView(viewModels);
        }

        public ActionResult Manage(string idUrl)
        {
            try
            {
                int id = int.Parse(idUrl);

                var order = DB.GetEntityById<Order>(id) as Order;

                if (order != null)
                {
                    var offices = DB.GetList<Office>().ToList();
                    var orders = DB.GetList<Order>().Where(o => o.UserID == order.UserID).ToList();
                    var problems = DB.GetList<OrderProblem>().Where(o => o.Order.UserID == order.UserID).ToList();
                    var orderProblemViewModels = new List<OrderProblemViewModel>();

                    order.OfficeStart = offices.SingleOrDefault(o => o.ID == order.OfficeIdStart);
                    order.OfficeEnd = offices.SingleOrDefault(o => o.ID == order.OfficeIdEnd);

                    foreach(var ord in orders)
                    {
                        var problem = problems.SingleOrDefault(p => p.OrderID == ord.ID);
                        var problemText = "No problems";
                        var fine = "";
                        if(problem != null)
                        {
                            problemText = problem.Text;
                            fine = problem.Fine.ToString();
                        }

                        var orderProblemViewModel = new OrderProblemViewModel()
                        {
                            ID = ord.ID,
                            BrandModel = ord.Car.BrandModel,
                            Price = ord.Price,
                            Status = ord.Status,
                            UserName = ord.User.FirstName + " " + ord.User.LastName,
                            Problem = problemText,
                            Fine = fine
                        };

                        orderProblemViewModels.Add(orderProblemViewModel);
                    }


                    var viewModel = new OrderManageViewModel()
                    {
                        CurrentOrder = order,
                        User = order.User,
                        OrderConfirmDeny = DB.GetList<OrderConfirmDeny>().SingleOrDefault(ocd => ocd.OrderID == order.ID) ?? new OrderConfirmDeny(),
                        OrderProblemViewModelsUserOrders = orderProblemViewModels,
                        AdditionalOptions = JsonConvert.DeserializeObject<List<AdditionalOption>>(order.AdditionalOptionsJson)
                    };

                   
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction("OrderNotFound", "Error");
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

        public ActionResult UpdateStatus(string idUrl, string carID, string cityID)
        {
            try
            {
                int id = int.Parse(idUrl);
               
                var order = DB.GetEntityById<Order>(id) as Order;
              
                if (order != null)
                {
                    switch (order.Status)
                    {
                        case "Waiting for manager review":
                            {
                                order.Status = "Waiting for customer confirm";
                                DB.Update<Order>(order.ID);
                            }
                            break;                  

                        case "Waiting for customer confirm":
                            {
                                int cityId = int.Parse(cityID);
                                int carId = int.Parse(carID);
                                var city = DB.GetEntityById<City>(cityId) as City;
                                var car = DB.GetEntityById<Car>(carId) as Car;

                                if (city != null && car != null)
                                {
                                    
                                    //var stock = DB.GetList<Stock>().First(s => s.CarID == carId && s.CityID == cityId && s.RentStartDateTime == null);
                                    //stock.RentStartDateTime = order.RentStartDateTime;
                                    //stock.RentFinishDateTime = order.RentFinishDateTime;
                                    //DB.Update<Stock>(stock.ID);

                                    order.Status = "Waiting for execution";
                                    //order.StockID = stock.ID;
                                    DB.Update<Order>(order.ID);
                                }
                                else
                                {
                                    return RedirectToAction("WrongUrl", "Error");
                                }
                            }
                            break;

                        case "Waiting for execution":
                            {
                                order.Status = "On execution";
                                DB.Update<Order>(order.ID);
                            }
                            break;

                        case "On execution":
                            {

                            }
                            break;

                        case "Exucuted":
                            {

                            }
                            break;
                        default:
                            {
                                return null;
                            }                          
                    }

                    return RedirectToAction("Orders");
                }
                else
                {
                    RedirectToAction("OrderNotFound", "Error");
                }

                return null;
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

        //public ActionResult DenyOrder(OrderConfirmDeny deny)
        public ActionResult DenyOrder(OrderManageViewModel viewModel)
        {
            if (viewModel != null)
            {
                var deny = viewModel.OrderConfirmDeny;
                var order = DB.GetEntityById<Order>(deny.OrderID) as Order;

                order.Status = "Denied";
                DB.Update<Order>(order.ID);

                DB.Save<OrderConfirmDeny>(deny);

                return RedirectToAction("Orders");

            }
            else
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }
    }
}