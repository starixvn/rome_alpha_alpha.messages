namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Alpha.Messages.Site.Requests;
    using Alpha.Web.Filters;
    using Announcements.Models;
    using MediatR;
    using Requests.NewMessage;

    public class NewMessageController : SiteBaseController
    {
        private readonly IParam specificationParam;

        public NewMessageController(IMediator mediator, IParam specificationParam) : base(mediator)
        {
            this.specificationParam = specificationParam;
        }

        [AllowProtectedMode]
        public ActionResult GetNumberOfNewAnnouncement(NumberOfNewMessageQuery message)
        {
            if (IsDisableMessages(message))
            {
                return new EmptyResult();
            }

            message.Specification = specificationParam;

            return Json(message);
        }

        private static bool IsDisableMessages(MessageHistoryBaseRequest message)
            => Configurations.AppSettings.DisableMessages || message.IsProtectedMode;
    }
}