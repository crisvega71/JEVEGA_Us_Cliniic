using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JEVEGA_Us_Cliniic.Controllers;

namespace JEVEGA_Us_Cliniic.Test
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            //Arrange
            HomeController controllerHome = new HomeController();

            // Act
            ViewResult result = controllerHome.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void About()
        {
            //Arrange
            HomeController controllerHome = new HomeController();

            // Act
            ViewResult result = controllerHome.About() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Services()
        {
            //Arrange
            HomeController controllerHome = new HomeController();
            // Act
            ViewResult result = controllerHome.Services() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DiagnosticServices()
        {
            //Arrange
            HomeController controllerHome = new HomeController();
            // Act
            ViewResult result = controllerHome.DiagnosticServices() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Vision()
        {
            //Arrange
            HomeController controllerHome = new HomeController();
            // Act
            ViewResult result = controllerHome.Vision() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DataPrivacy()
        {
            //Arrange
            HomeController controllerHome = new HomeController();
            // Act
            ViewResult result = controllerHome.DataPrivacy() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

    } //-- end class

} //--
