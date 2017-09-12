using System.Web.Mvc;
using Alpha.Messages.Site.Controllers;
using NUnit.Framework;

namespace Alpha.Messages.Site.Tests.Controllers
{
    [TestFixture]
    public class BaioAnnouncementControllerTest
    {
        [Test]
        public void BaioAnnouncement_Index_AlwaysReturnView()
        {
            var controller = new BaioAnnouncementController();
            var actionResult = controller.Index();

            Assert.IsInstanceOf(typeof(ActionResult), actionResult);
        }
    }
}