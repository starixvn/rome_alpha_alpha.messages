namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Alpha.Web.Filters;
    using Announcements.Models;
    using MediatR;
    using Requests.Announcement;
    using Sunplus.Agent.Model.Entities;

    public class AnnouncementController : SiteBaseController
    {
        private readonly IParam specificationParam;

        public AnnouncementController(IMediator mediator, IUserSession userSession, IParam specificationParam)
            : base(mediator, userSession)
        {
            this.specificationParam = specificationParam;
        }

        [AllowProtectedMode]
        public ActionResult Index(AnnouncementsHistoryQuery message)
        {
            message.Specification = specificationParam;
            ViewData["IsProtectedMode"] = message.IsProtectedMode;

            return View(message);
        }

        [AllowProtectedMode]
        public ActionResult Popup(PopupAnnouncementQuery message)
        {
            if (message.IsProtectedMode)
            {
                return new EmptyResult();
            }

            message.Specification = specificationParam;

            return View(message);
        }
    }
}