namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data.Repository;

    public class CountPrivateMessageCriteria : CriteriaBase
    {
        public CountPrivateMessageCriteria(int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public int UserId { get; }

        public string UserName { get; }

        public override string GetSettingKey() => "Dot_Msg_GetPriMsgCount";

        public override bool IsValid() => UserId >= 0 && !string.IsNullOrEmpty(UserName);
    }
}
