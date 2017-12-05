namespace Alpha.Messages.Site.Controllers
{
    using System;
    using System.Web.Mvc;
    using Configurations;
    using Constants;
    using MediatR;
    using Requests.Message;
    using Sunplus.Agent.Model.Entities;
    using Web;

    public class MessageController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IUserSession userSession;

        public MessageController(IMediator mediator, IUserSession userSession)
        {
            this.mediator = mediator;
            this.userSession = userSession;
        }       

        [HttpPost]
        public ActionResult GetLatestMessage()
        {
            var query = new LatestMessageQuery(userSession, false);

            return Json(mediator.Send(query));
        }

        [HttpPost]
        public ActionResult UpdatePrivateMessageAsRead()
        {
            var query = new UpdatePrivateMessageAsReadQuery(userSession);

            return Json(mediator.Send(query));
        }

        public ActionResult Index(int messageType = MessageType.Private)
        {
            var viewModel = mediator.Send(new MessageQuery(
                userSession,
                messageType,
                DateTime.Today.AddDays(-3),
                DateTime.Today,
                AppSettings.LocalTest));

            return View(viewModel);
        }

        public ActionResult Messages(
            DateTime? startDate,
            DateTime? endDate,
            int messageType = MessageType.Public)
        {
            var viewModel = mediator.Send(new MessageQuery(
                userSession,
                messageType,
                startDate ?? DateTime.Today.AddDays(-3),
                endDate ?? DateTime.Today,
                AppSettings.LocalTest));

            return View("Index", viewModel);
        }
    }
}