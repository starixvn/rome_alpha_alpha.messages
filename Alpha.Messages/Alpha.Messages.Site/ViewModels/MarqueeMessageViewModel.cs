namespace Alpha.Messages.Site.ViewModels
{
    using Alpha.Messages.Models.Message;

    public class MarqueeMessageViewModel
    {        
        public MarqueeMessageViewModel(
            GeneralMarqueeMessage publicMessage, 
            GeneralMarqueeMessage privateMessage)
        {
            PublicMessage = publicMessage;
            PrivateMessage = privateMessage;
        }

        public GeneralMarqueeMessage PublicMessage { get; }

        public GeneralMarqueeMessage PrivateMessage { get; }
       
    }
}