namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Alpha.Web;
    using MediatR;
    using Requests.MarqueeMessage;
    using Sunplus.Agent.Model.Entities;

    public class MarqueeMessageController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IUserSession userSession;

        public MarqueeMessageController(IMediator mediator, IUserSession userSession)
        {
            this.mediator = mediator;
            this.userSession = userSession;
        }

        [HttpPost]
        public ActionResult Index()
        {
            var query = new MarqueeMessageQuery(userSession, false);

            return Json(mediator.Send(query));
        }

        public ActionResult MoveBet()
            => Json(mediator.Send(new MoveBetMessageQuery(userSession)), JsonRequestBehavior.AllowGet);
    }
}
