using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Clockwork.Web;
using Clockwork.Web.Controllers;
using Clockwork.Web.Models;


namespace Clockwork.Web.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        //test the index action result to pull in version name and runtime name
        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            var result = (ViewResult)controller.Index("");

            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            var expectedVersion = mvcName.Version.Major + "." + mvcName.Version.Minor;
            var expectedRuntime = isMono ? "Mono" : ".NET";

            // Assert
            Assert.AreEqual(expectedVersion, result.ViewData["Version"]);
            Assert.AreEqual(expectedRuntime, result.ViewData["Runtime"]);
        }

        //make sure all available of the time zones are displayed
        [Test]
        public void ValidateTimeZones()
        {
            
            List<SelectListItem> testList = CurrentTimeQuery.GetTimeZones();
            int result = testList.Count;
            Assert.AreEqual(593, result);
            Assert.AreEqual("--", testList[0].Text);
        }

        //
    }
}
