namespace Alpha.Messages.Site.ViewModels.PortalMessage
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PortalMessagesViewModel
    {
        public PortalMessagesViewModel(List<PortalMessageWrapper> noticeMessages)
        {
            NoticeMessages = noticeMessages
                .Where(m => m.CurrentMessage.TopLevel >= (byte)MessageLevel.Range.Normal)
                .OrderByDescending(m => m.CurrentMessage.PostTime)
                .ToList();

            HighlightMessages = noticeMessages
                .Where(m => m.CurrentMessage.TopLevel <= (byte)MessageLevel.Range.Highlight)
                .OrderBy(m => m.CurrentMessage.TopLevel)
                .ThenByDescending(m => m.PostTime)
                .ToList();

            LastMessagePostTime = noticeMessages.Any() ? noticeMessages.Last().PostTime : DateTime.MinValue;
        }

        public List<PortalMessageWrapper> NoticeMessages { get; }

        public List<PortalMessageWrapper> HighlightMessages { get; }

        public DateTime LastMessagePostTime { get; }
    }
}