namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
    using Alpha.Messages.Models.Message;
    using Alpha.Messages.Site.ViewModels;
    using Alpha.Web.Filters;
    using MediatR;
    using Requests.MarqueeMessage;
    using Sunplus.Agent.Model.Entities;

    public class MarqueeMessageController : SiteBaseController
    {
        private readonly IUserSession userSession;

        public MarqueeMessageController(IMediator mediator, IUserSession userSession)
            : base(mediator)
        {
            this.userSession = userSession;
        }

        [HttpPost]
        [AllowProtectedMode]
        public ActionResult Index()
        {
            var viewModel = userSession.IsProtectedMode
                ? new MarqueeMessageViewModel(
                    new GeneralMarqueeMessage(),
                    new GeneralMarqueeMessage())
                : mediator.Send(new MarqueeMessageQuery(userSession));

            return Json(viewModel);
        }
    }
}
