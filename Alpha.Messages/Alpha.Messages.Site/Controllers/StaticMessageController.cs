namespace Alpha.Messages.Site.Controllers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Web.Mvc;
    using Alpha.Messages.Models.Announcement;
    using Alpha.Messages.Models.Constants;
    using Alpha.Web.Filters;
    using Sunplus.Agent.Model.Entities;
    using Web;

    [AllowProtectedMode]
    public class StaticMessageController : BaseController
    {
        private readonly IDictionary<string, string> FileNameMapping = new Dictionary<string, string>
        {
            ["167819"] = "TelegramChatbot"
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

            var fileDownloadName = FileNameMapping.ContainsKey(id) 
                ? $"{resourceManager.GetString(FileNameMapping[id], CultureInfo.CreateSpecificCulture(UserSession.Current.Language))}.pdf" 
                : "Guide.pdf";

            Response.AddHeader("Content-Disposition", "inline; filename=" + fileDownloadName);

            return base.File(Server.MapPath($"~/Views/StaticMessage/{fileName}.pdf"), "application/pdf");
        } 
    }
}