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
            string content)
        {
            Id = id;
            Content = content.Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("'", "\'");
        }

        public int Id { get; }

        public string Content { get; }
    }
}