namespace Alpha.Messages.Models.Message
{
    using System;
    using Alpha.Messages.Utils;
    using Messages.Constants;

    public class MessageHistory : MultiLanguague
    {
        public MessageHistory()
        {
        }

        public MessageHistory(
            int id,
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentKO,
            string contentVI,
            DateTime postTime,
            byte messageType,
            bool isSticky,
            string bettransId,
            long rowNum) : base(contentEN, contentCS, contentCH, contentTH, contentJP, contentKO, contentVI)
        {
            Id = id;
            PostTime = postTime;
            MessageType = messageType;
            IsSticky = isSticky;
            RowNum = rowNum;
            BetTransId = bettransId;
        }

        public int Id { get; private set; }

        public DateTime PostTime { get; internal set; }

        public byte MessageType { get; internal set; }

        public bool IsSticky { get; internal set; }

        public string BetTransId { get; }
        public int TotalMessasge { get; set; }

        public long RowNum { get; }

        public bool HasRedColor => ContentEN.Contains(MessageConstants.ContentFontStartMarkup);

        public string Content(string language)
        {
            var rawContent = GetContentByKey(language);

            if (MessageType == Messages.Constants.MessageType.Personal)
            {
                rawContent = MessageFormatter.MarkBoldText(rawContent, language);
            }

            return rawContent
                .Replace(MessageConstants.ContentFontStartMarkup, string.Empty)
                .Replace(MessageConstants.ContentFontEndMarkup, string.Empty)
                .Replace("@@", BetTransId);
        }
    }
}