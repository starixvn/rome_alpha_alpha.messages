namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Announcements.Models;
    using MediatR;
    using Requests.Announcement;

    public class AnnouncementController : SiteBaseController
    {
        private readonly IParam specificationParam;

        public AnnouncementController(IMediator mediator, IParam specificationParam) : base(mediator)
        {
            this.specificationParam = specificationParam;
        }

        public ActionResult Index(AnnouncementsHistoryQuery message)
        {
            message.Specification = specificationParam;
            
            return View(message);
        }

        public ActionResult Popup(PoupAnnouncementQuery message)
        {
            message.Specification = specificationParam;

            return View(message);
        }
    }
}