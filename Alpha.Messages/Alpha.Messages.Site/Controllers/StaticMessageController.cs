namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Alpha.Messages.Models.Constants;
    using Alpha.Web.Filters;
    using Sunplus.Agent.Model.Entities;
    using Web;

    [AllowProtectedMode]
    public class StaticMessageController : BaseController
    {
        public StaticMessageController(IUserSession userSession)
            : base(userSession)
        {
        }

        public ActionResult NewSportType()
        {
            AssignDarkModeViewData();
            return View();
        }

        public ActionResult Index(string id)
        {
            AssignDarkModeViewData();
            return View(id);
        }

        public ActionResult File(string id)
        {
            string viewName = $"{id}.{UserSession.Current.Language}";
            if (!System.IO.File.Exists(Server.MapPath($"~/Views/StaticMessage/{viewName}.cshtml")))
            {
                viewName = $"{id}.en-US";
            }

            return PartialView(viewName);
        }

        public ActionResult Guide(string id)
        {
            var roleName = UserLevel.GetDisplayName(UserSession.Current.RoleId);
            string fileName = $"{id}.{roleName}.{UserSession.Current.Language}";
            if (!System.IO.File.Exists(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf")))
            {
                fileName = $"{id}.{roleName}.en-US";
            }

            return base.File(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf"), "application/pdf");
        }
    }
}