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
        public ActionResult NewSportType()
            => View();

        public ActionResult Index(string id)
            => View(id);

        public ActionResult File(string id)
        {
            string viewName = $"{id}.{UserSession.Current.Language}";
            if (!System.IO.File.Exists(Server.MapPath($"~/Views/StaticMessage/{viewName}.cshtml")))
            {
                viewName = $"{id}.en-US";
            }

            if (!System.IO.File.Exists(Server.MapPath($"~/Views/StaticMessage/{viewName}.cshtml")))
            {
                return new EmptyResult();
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

            if (!System.IO.File.Exists(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf")))
            {
                return new EmptyResult();
            }

            return base.File(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf"), "application/pdf");
        }
    }
}