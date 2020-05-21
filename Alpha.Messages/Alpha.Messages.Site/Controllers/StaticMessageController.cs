namespace Alpha.Messages.Site.Controllers
{
    using Sunplus.Agent.Model.Entities;
    using System.Web.Mvc;
    using Web;

    public class StaticMessageController : BaseController
    {
        public ActionResult NewSportType() => View();

        public ActionResult Index(string id) => View(id);

        public ActionResult File(string id)
            => PartialView($"{id}.{UserSession.Current.Language}");
    }
}