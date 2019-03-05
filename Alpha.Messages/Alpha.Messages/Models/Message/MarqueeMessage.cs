namespace Alpha.Messages.Models.Message
{
    public class MarqueeMessage : MultiLanguague
    {
        public MarqueeMessage()
        {

        }
        public MarqueeMessage(
            int messageId,
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentKO,
            string contentVi,
            bool isBatch,
            string betTransId,
            int userId,
            bool isSticky,
            int msgType) : base(contentEN, contentCS, contentCH, contentJP, contentTH, contentKO, contentVi)
        {
            Id = messageId;

            BetTransId = betTransId;
            IsBatch = isBatch;
            IsSticky = isSticky;
            MsgType = msgType;
        }

        public int Id { get; }

        public string BetTransId { get; }

        public bool IsBatch { get; }

        public int MsgType { get; }

        public bool IsSticky { get; }
    }
}