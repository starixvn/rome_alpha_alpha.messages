namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
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
    }
}