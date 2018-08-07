namespace Alpha.Messages.Models.Message
{
    using System;

    public class MarqueeMessage : MultiLanguague
    {
        public MarqueeMessage(int messageId,
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentIT,
            string contentKO,
            string contentES,
            string contentVi,
            string contentID,
            string contentEL,
            string contentMX,
            DateTime postTime,
            DateTime createTime,
            DateTime lastUpdateTime,
            byte messageType,
            bool isSticky,
            bool isTest,
            int matchId,
            bool isBatch,
            int creator,
            int lastUpdator,
            string creatorName,
            string lastUpdateName,
            string userName,
            string betTransId,
            int userId) : base(contentEN, contentCS, contentCH, contentJP, contentTH, contentKO, contentVi)
        {
            Id = messageId;
            PostTime = postTime;
            CreateTime = createTime;
            LastUpdateTime = lastUpdateTime;
            MessageType = messageType;
            IsSticky = isSticky;
            CreatorId = creator;
            LastUpdator = lastUpdator;
            CreatorName = creatorName;
            LastUpdateName = lastUpdateName;
            UserId = userId;
            UserName = userName;
            ReplaceBetTransId(betTransId, isBatch);
        }

        public int Id { get; private set; }

        public DateTime PostTime { get; private set; }

        public DateTime CreateTime { get; private set; }

        public DateTime LastUpdateTime { get; private set; }

        public int MessageType { get; private set; }

        public bool IsSticky { get; private set; }

        public int CreatorId { get; private set; }

        public int LastUpdator { get; private set; }

        public string CreatorName { get; private set; }

        public string LastUpdateName { get; private set; }

        public int UserId { get; private set; }

        public string UserName { get; private set; }        
    }
}