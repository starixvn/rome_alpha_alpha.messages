namespace Alpha.Messages.Models.Message
{
    using System;

    public class PortalMessage : MultiLanguague
    {
        public PortalMessage()
        {

        }

        public PortalMessage(
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentKO,
            string contentVI,
            DateTime postTime,
            byte type,
            bool isSticky,
            string betTransId,
            byte topLevel) : base(contentEN, contentCS, contentCH, contentTH, contentJP, contentKO, contentVI)
        {
            PostTime = postTime;
            MessageType = type;
            IsSticky = isSticky;
            BetTransId = betTransId;
            TopLevel = topLevel;
        }
        public DateTime PostTime { get; }

        public byte MessageType { get; }

        public bool IsSticky { get; }

        public string BetTransId { get; }

        public byte TopLevel { get; }
    }
}