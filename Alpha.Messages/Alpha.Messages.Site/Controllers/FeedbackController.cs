namespace Alpha.Messages.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.Messages.Site.Requests.Feedback;
    using Alpha.Web;
    using MediatR;
    using Sunplus.Agent.Model.Entities;

    public class FeedbackController : BaseController
    {
        private readonly IMediator mediator;
        private readonly IUserSession userSession;

        public FeedbackController(IMediator mediator, IUserSession userSession)
        {
            this.mediator = mediator;
            this.userSession = userSession;
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View("~/Views/Feedback/Index.cshtml");
        }

        public async Task<ActionResult> Add(AddFeedbackQuery query)
        {
            query.CustId = userSession.CustId;
            query.SubAccId = userSession.SubAccId;

            var model = await mediator.SendAsync(query);

            return Json(model);
        }
    }
}