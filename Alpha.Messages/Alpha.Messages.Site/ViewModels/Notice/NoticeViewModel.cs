namespace Alpha.Messages.Site.ViewModels
{
    public class NoticeViewModel
    {
        public NoticeViewModel(int totalMessage)
        {
            TotalMessage = totalMessage;
        }

        public int TotalMessage { get; }
    }
}