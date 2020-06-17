using System;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JEVEGA_Us_Cliniic.Controllers;

namespace JEVEGA_Us_Cliniic.Test
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void Login()
        {
            //Arrange
            UsersController controllerUser = new UsersController();
            // Act
            ViewResult result = controllerUser.Login() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Login_Test2()
        {
            //Arrange
            UsersController controllerUser = new UsersController();
            User userInfo = new User();
            userInfo.Username = "crisvega";
            userInfo.Password = "CrvMA8903510";

            // Act
            ViewResult result = controllerUser.Login(userInfo, "PatientData/Index") as ViewResult;
            
            // Assert
            Assert.IsNotNull(result);
        }

    }
}
