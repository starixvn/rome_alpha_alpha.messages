namespace Alpha.Messages.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Filter;
    using MediatR;
    using Requests;
    using ViewModels;

    [HmacAuthorizationFilter]
    public class AnnouncementController : ApiController
    {
        private readonly IMediator mediator;

        public AnnouncementController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("announcements")]
        public IEnumerable<AnnouncementViewModel> GetAnnouncements([FromBody]GetAnnouncementsQuery message)
            => mediator.Send(message);
    }
}