namespace Alpha.Messages.Models.Message
{
    public class GeneralMarqueeMessage
    {
        public GeneralMarqueeMessage()
        {
            Id = 0;
            Content = string.Empty;
        }

        public GeneralMarqueeMessage(
            int id,
            string content,
            string betTransId,
            bool isBatch,
            bool isSticky,
            int msgTypeFromDB)
        {
            Id = id;
            Content = content.Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("'", "\'");

            if (isBatch)
            {
                Content = Content.Replace("@@", betTransId);
            }

            MessageType = isSticky ? Messages.Constants.MessageType.Special : msgTypeFromDB == 3 ? Messages.Constants.MessageType.Normal: Messages.Constants.MessageType.Personal;
        }

        public int Id { get; }

        public string Content { get; }

        public int MessageType { get; }
    }
}