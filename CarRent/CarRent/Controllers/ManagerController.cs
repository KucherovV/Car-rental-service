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
            var orders = DB.GetList<Order>()/*.Where(o => o.IsArchived == false)*/.ToList();
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
                    case "On execution":
                        {
                            color = "purple";
                        }
                        break;
                    case "Denied":
                        {
                            color = "gray";
                        }
                        break;
                    case "Executed":
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


                    var problems = DB.GetList<OrderProblem>()/*.Where(o => o.Order.UserID == order.UserID)*/.ToList();
                    foreach(var problem in problems)
                    {
                        problem.Order = DB.GetEntityById<Order>(problem.Order_ID) as Order;
                    }


                    var orderProblemViewModels = new List<OrderProblemViewModel>();

                    order.OfficeStart = offices.SingleOrDefault(o => o.ID == order.OfficeIdStart);
                    order.OfficeEnd = offices.SingleOrDefault(o => o.ID == order.OfficeIdEnd);

                    foreach(var ord in orders)
                    {
                        var problem = problems.SingleOrDefault(p => p.Order_ID == ord.ID);
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
                        AdditionalOptions = JsonConvert.DeserializeObject<List<AdditionalOption>>(order.AdditionalOptionsJson),
                        OrderProblem = DB.GetList<OrderProblem>().SingleOrDefault(op => op.Order_ID == order.ID)
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

        public ActionResult UpdateStatus(string idUrl, string cityID, int? stockId)
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
                                order.Status = "Waiting for execution";
                                //order.StockID = stock.ID;
                                DB.Update<Order>(order.ID);                                                        
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
                                if (stockId != null)
                                {
                                    order.Status = "Executed";
                                    order.IsArchived = true;
                                    DB.Update<Order>(order.ID);

                                    var stock = DB.GetEntityById<Stock>((int)stockId) as Stock;
                                    stock.IsBusy = false;
                                    DB.Update<Stock>(stock.ID);
                                }
                                else
                                {
                                    RedirectToAction("OrderNotFound", "Error");
                                }
                            }
                            break;

                        case "Executed":
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

        public ActionResult DenyOrder(OrderManageViewModel viewModel)
        {
            if (viewModel != null)
            {
                var deny = viewModel.OrderConfirmDeny;
                var order = DB.GetEntityById<Order>(deny.OrderID) as Order;

                order.Status = "Denied";
                DB.Update<Order>(order.ID);

                DB.Save<OrderConfirmDeny>(deny);

                var stock = DB.GetEntityById<Stock>((int)viewModel.CurrentOrder.StockID) as Stock;
                stock.IsBusy = false;
                DB.Update<Stock>(stock.ID);

                return RedirectToAction("Orders");
            }
            else
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }

        public ActionResult OrderProblem(OrderManageViewModel viewModel)
        {
            if (viewModel != null)
            {
                var problem = viewModel.OrderProblem;
                problem.Order_ID = viewModel.OrderConfirmDeny.OrderID;
                problem.UserID = viewModel.CurrentOrder.UserID;
                
                var order = DB.GetEntityById<Order>(viewModel.OrderConfirmDeny.OrderID) as Order;
                order.Status = "Executed";
                order.IsArchived = true;

                var user = DB.GetEntityById<ApplicationUser>(viewModel.CurrentOrder.UserID) as ApplicationUser;
                user.Fine += problem.Fine;

                var stock = DB.GetEntityById<Stock>((int)viewModel.CurrentOrder.StockID) as Stock;
                stock.IsBusy = false;

                DB.Update<Order>(order.ID);
                DB.Save<OrderProblem>(problem);
                DB.Update<Stock>(stock.ID);
                DB.Update<ApplicationUser>(viewModel.CurrentOrder.UserID);

                return RedirectToAction("Orders");
            }
            else
            {
                return RedirectToAction("WrongUrl", "Error");
            }
        }
    }
}