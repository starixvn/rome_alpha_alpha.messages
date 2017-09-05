namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetLatestPrivateMessageCriteria : CriteriaBase
    {
        public GetLatestPrivateMessageCriteria(int userId, bool isTest)
        {
            UserId = userId;
            IsTest = isTest;
            LatestMessageId = 0;
        }

        public int UserId { get; }

        [SpParam(SqlParamName = "latestmsgid")]
        public int LatestMessageId { get; }

        public bool IsTest { get; }

        public override string GetSettingKey() => "Dot_Msg_GetTopPriMessage24";

        public override bool IsValid() => UserId > 0;
    }
}
