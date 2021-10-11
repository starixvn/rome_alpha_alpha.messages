namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Alpha.Web.Filters;
    using Announcements.Models;
    using MediatR;
    using Requests.Announcement;

    public class AnnouncementController : SiteBaseController
    {
        private readonly IParam specificationParam;

        public AnnouncementController(IMediator mediator, IParam specificationParam)
            : base(mediator)
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