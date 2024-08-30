namespace Alpha.Messages.Site.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Mvc;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Models.Constants;
    using Alpha.Web.Filters;
    using AlphaPlus.User.Models;
    using Web;

    [AllowProtectedMode]
    public class StaticMessageController : BaseController
    {
        private readonly IDictionary<string, string> FileNameMapping = new Dictionary<string, string>
        {
            ["167819"] = "TelegramChatbot",
            ["167820"] = "TelegramChatbot",
            ["170467"] = "MyWatchlist"
        };

        private readonly ResourceManager resourceManager;

        public StaticMessageController(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

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
                if (!System.IO.File.Exists(Server.MapPath($"~/Views/StaticMessage/{id}.cshtml")))
                {
                    return new EmptyResult();
                }

                return PartialView(id);
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

            var fileDownloadName = FileNameMapping.ContainsKey(id)
                ? $"{resourceManager.GetString(FileNameMapping[id], CultureInfo.CreateSpecificCulture(UserSession.Current.Language))}.pdf"
                : "Guide.pdf";

            Response.AddHeader("Content-Disposition", "inline; filename=" + fileDownloadName);

            return base.File(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf"), "application/pdf");
        }

        public ActionResult Help(string id)
        {
            string fileName = $"{id}.{UserSession.Current.Language}";
            if (!System.IO.File.Exists(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf")))
            {
                fileName = $"{id}.en-US";
            }

            if (!System.IO.File.Exists(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf")))
            {
                return new EmptyResult();
            }

            var fileDownloadName = FileNameMapping.ContainsKey(id)
                ? $"{resourceManager.GetString(FileNameMapping[id], CultureInfo.CreateSpecificCulture(UserSession.Current.Language))}.pdf"
                : "Guide.pdf";

            Response.AddHeader("Content-Disposition", "inline; filename=" + fileDownloadName);

            return base.File(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf"), "application/pdf");
        }
    }
}