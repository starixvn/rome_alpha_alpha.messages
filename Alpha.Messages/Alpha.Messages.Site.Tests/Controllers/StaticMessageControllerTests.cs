using Alpha.Messages.Site.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Alpha.Messages.Site.Tests.Controllers
{
    [TestFixture]
    public class StaticMessageControllerTests
    {
        private StaticMessageController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new StaticMessageController();
        }

        [TestCase("102924", "102924")]
        [TestCase("112894", "112894")]
        [TestCase("117068", "117068")]
        public void Index_Always_CallsToGetMessage(string id, string viewName)
        {
            // Act
            var act = controller.Index(id);

            // Assert
            Assert.AreEqual(((ViewResult)act).ViewName, viewName);
        }

        [Test]
        public void NewSportType_Always_ReturnView()
        {
            // Act
            var act = controller.NewSportType();

            // Assert
            Assert.IsTrue(act is ViewResult);
        }
    }
}
