namespace Alpha.Messages.Models.Message
{
    public class GeneralLatestMessage
    {
        public GeneralLatestMessage()
        {
            Id = 0;
            Content = string.Empty;
        }

        public GeneralLatestMessage(
            int id,
            string content)
        {
            Id = id;
            Content = content.Replace("\n", string.Empty)
                .Replace("\r", string.Empty)
                .Replace("'", "\'");
        }

        public int Id { get; private set; }

        public string Content { get; private set; }
    }
}