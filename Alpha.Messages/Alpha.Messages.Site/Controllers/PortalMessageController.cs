namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Alpha.Messages.Configurations;
    using MediatR;
    using Messsages.Site.Requests.PortalMessage;
    using Sunplus.Agent.Model.Entities;

    public class PortalMessageController : SiteBaseController
    {
        private const int DefaultNumberMessage = 10;

        public PortalMessageController(IMediator mediator, IUserSession userSession)
            : base(mediator, userSession)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (AppSettings.DisableMessages)
            {
                return new EmptyResult();
            }

            return View(DefaultNumberMessage);
        }

        [HttpGet]
        public ActionResult Messages(int totalMessage)
        {
            if (AppSettings.DisableMessages)
            {
                return new EmptyResult();
            }

            var viewModel = mediator.Send(
                new PortalMessageQuery(userSession, totalMessage, AppSettings.LocalTest));

            return PartialView("Messages", viewModel);
        }

        [HttpPost]
        public bool MarkMessageAsRead(int messageId)
        {
            if (AppSettings.DisableMessages)
            {
                return false;
            }

            var isSuccess = mediator.Send(new MarkMessageAsReadQuery(userSession.CustId, messageId, userSession.IsInternal));

            return isSuccess;
        }

        [HttpGet]
        public ActionResult MoreMessages(int totalMessage)
        {
            if (AppSettings.DisableMessages)
            {
                return new EmptyResult();
            }

            return View("Index", totalMessage);
        }
    }
}