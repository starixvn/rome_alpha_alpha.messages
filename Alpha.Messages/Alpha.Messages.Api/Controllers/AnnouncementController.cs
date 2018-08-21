namespace Alpha.Messages.Api.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using MediatR;
    using Requests;
    using ViewModels;

    public class AnnouncementController : ApiController
    {
        private readonly IMediator mediator;

        public AnnouncementController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public IEnumerable<AnnouncementViewModel> GetAnnouncements([FromBody]GetAnnouncementsQuery message)
            => mediator.Send(message);
    }
}