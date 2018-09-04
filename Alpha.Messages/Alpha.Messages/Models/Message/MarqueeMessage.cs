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
            int userId) : base(contentEN, contentCS, contentCH, contentJP, contentTH, contentKO, contentVi)
        {
            Id = messageId;
            ReplaceBetTransId(betTransId, isBatch);
        }

        public int Id { get;}     
    }
}