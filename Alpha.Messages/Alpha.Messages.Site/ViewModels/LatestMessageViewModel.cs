using Alpha.Messages.Models.Message;

namespace Alpha.Messages.Site.ViewModels.Message
{
    public class LatestMessageViewModel
    {        
        public GeneralLatestMessage PublicMessage { get; set; }

        public GeneralLatestMessage PrivateMessage { get; set; }

        public int NumberOfUnreadPrivateMessage { get; set; }

        public GeneralMoveBetsMessage MoveBetsInformation { get; set; }
    }
}