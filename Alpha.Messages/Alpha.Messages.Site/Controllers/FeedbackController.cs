namespace Alpha.Messages.Site.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.Messages.Site.Requests.Feedback;
    using Alpha.Web;
    using Alpha.Web.Filters;
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

        [HttpGet, AllowProtectedMode]
        public ViewResult Index()
        {
            return View("~/Views/Feedback/Index.cshtml");
        }

        [AllowProtectedMode]
        public async Task<ActionResult> CheckExist(CheckExistsFeedbackQuery query)
        {
            query.CustId = userSession.CustId;
            query.SubAccId = userSession.SubAccId;

            var model = await mediator.SendAsync(query);

            return Json(model);
        }

        [AllowProtectedMode]
        public async Task<ActionResult> Add(AddFeedbackQuery query)
        {
            query.CustId = userSession.CustId;
            query.SubAccId = userSession.SubAccId;
            query.IsInternal = userSession.IsInternal;

            var model = await mediator.SendAsync(query);

            return Json(model);
        }
    }
}