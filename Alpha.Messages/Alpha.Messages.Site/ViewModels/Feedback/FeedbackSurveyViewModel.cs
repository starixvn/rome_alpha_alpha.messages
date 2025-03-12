using Alpha.Messages.Site.Requests.Feedback;

namespace Alpha.Messages.Site.ViewModels.Feedback
{
    public class FeedbackSurveyViewModel
    {
        public FeedbackSurveyViewModel()
        {
        }

        public FeedbackSurveyViewModel(FeedbackSurveyQuery query)
        {
            NickName = query.NickName;
            IsInternal = query.IsInternal;
        }

        public string NickName { get; }
        public bool IsInternal { get; }
    }
}