using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Entities;
using DataBase;
using Newtonsoft.Json;
using CarRent.ViewModels;
using CarRent.VO;
using Microsoft.AspNet.Identity;

namespace CarRent.Controllers
{
    [Authorize(Roles = "moderator")]
    public class ManagerController : Controller
    {
        private readonly DB DB;

        public ManagerController(DB Db)
        {
            DB = Db;
        }
        public ManagerController()
        {
            DB = new DB();
        }

        ManagerControllerVO vo = new ManagerControllerVO();

        public ActionResult Orders()
        {
            var viewModel = new OrderListViewModel()
            {
                Statuses = new MultiSelectList(Enumerations.Statuses)
            };

            vo.OrderPageShown();

            return View(viewModel);
        }

        //download orders to ajax
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

            vo.OrdersDownloaded(viewModels.Select(vm => vm.ID).ToList());

            return PartialView(viewModels);
        }

        //return view vith managing info
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


                    var problems = DB.GetList<OrderProblem>().ToList();
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

                    vo.OrderPassedForManaging();

                    return View(viewModel);
                }
                else
                {
                    vo.OrderNotFound(idUrl);

                    return RedirectToAction("OrderNotFound", "Error");
                }
            }
            catch (ArgumentException)
            {
                vo.WrongUrl(idUrl);

                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                vo.WrongUrl(idUrl);

                return RedirectToAction("WrongUrl", "Error");
            }
        }

        //update status of order
        public ActionResult UpdateStatus(string idUrl, string cityID, int? stockId)
        {
            try
            {
                int id = int.Parse(idUrl);
               
                var order = DB.GetEntityById<Order>(id) as Order;
                order.OfficeEnd = DB.GetEntityById<Office>(order.OfficeIdEnd) as Office;
              
                if (order != null)
                {
                    switch (order.Status)
                    {
                        case "Waiting for manager review":
                            {
                                order.Status = "Waiting for customer confirm";
                                DB.Update<Order>(order.ID);

                                vo.ChangeStatus("Waiting for customer confirm");
                            }
                            break;                  

                        case "Waiting for customer confirm":
                            {                                      
                                order.Status = "Waiting for execution";
                                DB.Update<Order>(order.ID);

                                vo.ChangeStatus("Waiting for execution");
                            }
                            break;

                        case "Waiting for execution":
                            {
                                var user = DB.GetEntityById<ApplicationUser>(order.UserID) as ApplicationUser;
                                if(user.Debt > 0 || user.Fine > 0)
                                {
                                    int amount = user.Debt + user.Fine;
                                    return RedirectToAction("UserHasDebt", "Error", new { amount = amount });
                                }

                                order.Status = "On execution";
                                DB.Update<Order>(order.ID);

                                vo.ChangeStatus("On execution");
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
                                    stock.CityID = order.OfficeEnd.CityID;
                                    DB.Update<Stock>(stock.ID);

                                    NoticeSubscribers(order.CarID, stock.CityID);

                                    vo.ChangeStatus("Executed");
                                }
                                else
                                {
                                    vo.OrderNotFound(idUrl);

                                    RedirectToAction("OrderNotFound", "Error");
                                }
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
                    vo.OrderNotFound(idUrl);

                    RedirectToAction("OrderNotFound", "Error");
                }

                return null;
            }
            catch (ArgumentException)
            {
                vo.WrongUrl(idUrl);

                return RedirectToAction("WrongUrl", "Error");
            }
            catch (FormatException)
            {
                vo.WrongUrl(idUrl);

                return RedirectToAction("WrongUrl", "Error");
            }
        }

        //deny order
        public ActionResult DenyOrder(OrderManageViewModel viewModel)
        {
            if (viewModel != null)
            {
                var deny = viewModel.OrderConfirmDeny;
                DB.Save<OrderConfirmDeny>(deny);

                var order = DB.GetEntityById<Order>(deny.OrderID) as Order;
                var user = DB.GetEntityById<ApplicationUser>(order.UserID) as ApplicationUser;
                user.Debt -= order.Price;
                DB.Update<ApplicationUser>(user.Id);

                order.Status = "Denied";
                DB.Update<Order>(order.ID);

                var stock = DB.GetEntityById<Stock>((int)viewModel.CurrentOrder.StockID) as Stock;
                stock.IsBusy = false;
                DB.Update<Stock>(stock.ID);

                NoticeSubscribers(viewModel.CurrentOrder.CarID, stock.CarID);

                return RedirectToAction("Orders");
            }
            else
            {
                vo.WrongUrl(viewModel.CurrentOrder.StockID.ToString());

                return RedirectToAction("WrongUrl", "Error");
            }
        }

        //register order problem
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
                vo.WrongUrl(viewModel.CurrentOrder.StockID.ToString());

                return RedirectToAction("WrongUrl", "Error");
            }
        }

        //send emails to all car subscribers
        public void NoticeSubscribers(int carId, int cityId)
        {
            var car = DB.GetEntityById<Car>(carId) as Car;
            var city = DB.GetEntityById<City>(cityId) as City;

            var userIds = DB.GetList<UserWait>()
                .Where(uw => uw.CarID == carId && uw.CityID == cityId)
                .Select(s => s.UserID)
                .ToList();

            var emails = new List<string>();
            var users = DB.GetList<ApplicationUser>().ToList();
            foreach(var user in users)
            {
                if (userIds.Contains(user.Id))
                {
                    emails.Add(user.Email);
                }
            }

            foreach(var email in emails)
            {
                EmailService emailService = new EmailService();
                emailService.Send(new IdentityMessage()
                {
                    Body = "Car " + car.BrandModel + " that you subscribed is awiable for renting",
                    Destination = email,
                    Subject = "Car rect"
                });               
            }         
            
            foreach(var id in userIds)
            {
                var userWait = DB.GetList<UserWait>()
                    .SingleOrDefault(uw => uw.UserID == id && uw.CityID == cityId && uw.CarID == carId) as UserWait;
                if(userWait != null)
                {
                    DB.Delete<UserWait>(userWait);
                }
            }
        }
    }
}