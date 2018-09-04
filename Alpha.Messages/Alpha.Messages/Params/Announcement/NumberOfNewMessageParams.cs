using Alpha.Messages.Models.Message;

namespace Alpha.Messages.Params.Announcement
{
    public class NumberOfNewMessageParams : BaseAnnouncementsParams
    {
        public NumberOfUnreadMessage UnreadMessage { get; set; }
    }
}
