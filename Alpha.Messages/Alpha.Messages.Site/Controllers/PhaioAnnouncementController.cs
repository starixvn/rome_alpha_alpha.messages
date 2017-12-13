namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using System.Web.SessionState;
    using Web;

    [SessionState(SessionStateBehavior.Default)]
    public class PhaioAnnouncementController : BaseController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}