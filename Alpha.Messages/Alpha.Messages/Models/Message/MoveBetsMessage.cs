namespace Alpha.Messages.Models.Message
{
    using System;

    public class MoveBetsMessage : MultiLanguague
    {
        private const int MessageTimeZone = 8;        
       
        public MoveBetsMessage(
            string contentEN,
            string contentCS,
            string contentCN,
            string contentCH,
            string contentTH,
            string contentJP,
            string contentKO,
            string contentVI,
            DateTime moveBetEndTime) : base(contentEN, contentCS, contentCH, contentJP, contentTH, contentKO, contentVI)
        {
            MoveBetsEndTime = string.IsNullOrEmpty(base.ContentEN) ? string.Empty : new DateTimeOffset(moveBetEndTime, new TimeSpan(MessageTimeZone, 0, 0)).ToString();            
        }

        public string MoveBetsEndTime { get; }
    }
}