using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRent.Controllers;
using System.Web.Mvc;
using System.Collections.Generic;
using CarRent.ViewModels;
using Moq;
using DataBase;
using Entities;

namespace CarRent.Tests
{
    [TestClass]
    public class UserControlerTests
    {
        [TestMethod]
        public void GetCarsTest()
        {
            var repo = new Mock<DB>();
            repo.Setup(x => x.GetList<Car>()).Returns(new List<Car>());
            var userController = new UserController(repo.Object);

            

            //var result = userController.GetCars(null, null, null, null, null, null, null, null) as ActionResult;

            //Assert.IsNotNull(result);

            //view 

            //var controller = new UserController();

            ////var result = controller.GetCars(null, null, null, null, null, null, null, null) as ActionResult;
            //var result = controller.GetCars(null, null, null, null, null, null, null, null) as ViewResult;

            //Assert.IsNotNull(result);

            //var controller = new UserController();

            //var result = controller.GetCars(null, null, null, null, null, null, null, null) as ActionResult;

            //Assert.IsNotNull(result);
        }
    }
}
