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
            bool isBatch)
        {
            Id = id;
            Content = content.Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("'", "\'");

            if (isBatch)
            {
                Content = Content.Replace("@@", betTransId);
            }
        }

        public int Id { get; }

        public string Content { get; }
    }
}