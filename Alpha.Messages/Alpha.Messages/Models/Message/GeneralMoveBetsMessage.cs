namespace Alpha.Messages.Models.Message
{
    public class GeneralMoveBetsMessage
    {
        public GeneralMoveBetsMessage(
            string content,
            string moveBetEndTime)
        {
            Content = content;
            MoveBetsEndTime = moveBetEndTime;
        }

        public string Content { get; }

        public string MoveBetsEndTime { get; }
    }
}