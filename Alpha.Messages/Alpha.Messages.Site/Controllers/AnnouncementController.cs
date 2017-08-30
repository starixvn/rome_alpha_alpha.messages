namespace Alpha.Messages.Site.Controllers
{
    using Announcements.Models;
    using MediatR;
    using Requests;
    using System.Web.Mvc;

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