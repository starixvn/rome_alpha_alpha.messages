namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Announcements.Models;
    using MediatR;
    using Requests;

    public class AnnouncementController : SiteBaseController
    {
        private readonly IParam specificationParam;

        public AnnouncementController(IMediator mediator, IParam specificationParam) : base(mediator)
        {
            this.specificationParam = specificationParam;
        }

        public ActionResult PopupAnnouncement(PopupAnnouncementQuery message)
        {
            message.Specification = specificationParam;

            return View(message);
        }

        public ActionResult HeaderAnnouncement(HeaderAnnouncementQuery message)
        {
            message.Specification = specificationParam;

            return View(message);
        }
    }
}