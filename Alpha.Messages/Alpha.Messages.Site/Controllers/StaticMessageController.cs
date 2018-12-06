namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Web;

    public class StaticMessageController : BaseController
    {
        public ActionResult NewSportType() => View();

        public ActionResult Index(string id) => View(id);
    }
}