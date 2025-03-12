using System.Net.PeerToPeer;
using AlphaPlus.User.Models;

namespace Alpha.Messages.Site.Requests.Feedback
{
    public class FeedbackSurveyQuery
    {

        public FeedbackSurveyQuery(IUserSession userSession)
        {
            NickName = userSession.NickName;
            IsInternal = userSession.IsInternal;
        }

        public string NickName { get; set; }
        public bool IsInternal { get; set; }
    }
}