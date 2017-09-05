namespace Alpha.Messages.Models.Message
{
    using System;

    public class MoveBetsMessage : MultiLanguague
    {
        private const int MessageTimeZone = 8;
        public MoveBetsMessage(
            string contentEN,
            string contentCS,
            string contentCH,
            string contentTH,
            string contentJP,
            string contentKO,
            string contentVN,
            DateTime MoveBetEndTime) : base(contentEN, contentCS, contentCH, contentJP, contentTH, contentKO, contentVN)
        {
            MoveBetsEndTime = string.IsNullOrEmpty(ContentEN) ? string.Empty : new DateTimeOffset(MoveBetEndTime, new TimeSpan(MessageTimeZone, 0, 0)).ToString();            
        }

        public string MoveBetsEndTime { get; }
    }
}