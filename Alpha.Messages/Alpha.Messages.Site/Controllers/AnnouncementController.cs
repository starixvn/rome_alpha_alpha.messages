namespace Alpha.Messages.Site.Controllers
{
    using Announcements.Models;
    using Configurations;
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

        public ActionResult HotAnnouncement(HotAnnouncementQuery message)
        {
            message.NoticeTypes = AppSettings.NoticeTypes;
            message.IsMobileDevice = Request.Browser.IsMobileDevice;
            message.Specification = specificationParam;

            return View(message);
        }
    }
}