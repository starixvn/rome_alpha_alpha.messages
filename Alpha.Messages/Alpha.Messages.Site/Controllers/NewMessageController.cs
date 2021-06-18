namespace Alpha.Messages.Site.Controllers
{
    using System.Web.Mvc;
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
    
        public ActionResult GetNumberOfNewAnnouncement(NumberOfNewMessageQuery message)
        {
            if (Configurations.AppSettings.DisableMessages)
            {
                return new EmptyResult();
            }

            message.Specification = specificationParam;

            return Json(message);
        }        
    }
}