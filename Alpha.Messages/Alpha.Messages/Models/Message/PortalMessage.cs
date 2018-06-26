namespace Alpha.Messages.Models.Message
{
    using System;

    public class PortalMessage : MultiLanguague
    {
        public PortalMessage(
            int id,
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentIT,
            string contentKO,
            string contentES,
            string contentVI,
            DateTime postTime,
            byte type,
            bool isSticky,
            DateTime lastUpdateTime,
            string betTransId,
            bool isRead,
            byte topLevel) : base(contentEN, contentCS, contentCH, contentTH, contentJP, contentKO, contentVI)
        {
            Id = id;
            PostTime = postTime;
            MessageType = type;
            IsSticky = isSticky;
            LastUpdateTime = lastUpdateTime;
            BetTransId = betTransId;
            IsRead = isRead;
            TopLevel = topLevel;
        }

        public int Id { get; }

        public DateTime PostTime { get; }

        public byte MessageType { get; }

        public bool IsSticky { get; }

        public DateTime LastUpdateTime { get; }

        public string BetTransId { get; }

        public bool IsRead { get; }

        public byte TopLevel { get; }
    }
}