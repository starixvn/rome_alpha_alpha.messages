namespace Alpha.Messages.Site.ViewModels
{
    using Alpha.Messages.Models.Message;

    public class MarqueeMessageViewModel
    {        
        public MarqueeMessageViewModel(
            GeneralMarqueeMessage publicMessage, 
            GeneralMarqueeMessage privateMessage, 
            GeneralMoveBetsMessage moveBetsInformation)
        {
            PublicMessage = publicMessage;
            PrivateMessage = privateMessage;
            MoveBetsInformation = moveBetsInformation;
        }

        public GeneralMarqueeMessage PublicMessage { get; }

        public GeneralMarqueeMessage PrivateMessage { get; }
        
        public GeneralMoveBetsMessage MoveBetsInformation { get; }
    }
}