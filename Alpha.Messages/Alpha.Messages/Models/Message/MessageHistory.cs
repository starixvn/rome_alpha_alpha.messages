namespace Alpha.Messages.Models.Message
{
    using System;

    public class MessageHistory : MultiLanguague
    {
        public MessageHistory(
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
            DateTime createTime,
            DateTime lastUpdateTime,
            byte messageType,
            bool isSticky,
            bool isTest,
            int matchId,
            int creator,
            int lastUpdator,
            string creatorName,
            string lastUpdateName,
            bool isBatch,
            string bettransId,
            int userId,
            string userName,
            long rowNum) : base(contentEN, contentCS, contentCH, contentTH, contentJP, contentKO, contentVI)
        {
            Id = id;
            PostTime = postTime;
            CreateTime = createTime;
            LastUpdateTime = lastUpdateTime;
            MessageType = messageType;
            IsSticky = isSticky;
            IsTest = isTest;
            MatchId = matchId;
            CreatorId = creator;
            LastUpdator = lastUpdator;
            CreatorName = creatorName;
            LastUpdateName = lastUpdateName;
            IsBatch = isBatch;
            BetTransId = bettransId;
            UserId = userId;
            UserName = userName;
            RowNum = rowNum;
        }

        public int Id { get; private set; }
        
        public DateTime PostTime { get; private set; }

        public DateTime CreateTime { get; private set; }

        public DateTime LastUpdateTime { get; internal set; }

        public byte MessageType { get; internal set; }

        public bool IsSticky { get; internal set; }

        public bool IsTest { get; private set; }

        public int MatchId { get; private set; }

        public int CreatorId { get; private set; }

        public int LastUpdator { get; private set; }

        public string CreatorName { get; private set; }

        public string LastUpdateName { get; private set; }

        public bool IsBatch { get; private set; }

        public string BetTransId { get; private set; }

        public int UserId { get; private set; }

        public string UserName { get; private set; }

        public long RowNum { get; private set; }
    }
}