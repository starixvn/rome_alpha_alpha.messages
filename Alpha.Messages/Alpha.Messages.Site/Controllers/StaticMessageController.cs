namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Web;

    public class StaticMessageController : BaseController
    {
        // GET: NewSportType
        public ActionResult NewSportType()
        {
            return View();
        }
    }
}