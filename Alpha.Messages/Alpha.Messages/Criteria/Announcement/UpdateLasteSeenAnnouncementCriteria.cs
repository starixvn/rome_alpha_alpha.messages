namespace Alpha.Messages.Criteria.Announcement
{
    using Fanex.Data.Repository;

    public class UpdateLasteSeenAnnouncementCriteria : NonQueryCommand
    {
        public UpdateLasteSeenAnnouncementCriteria(int userId, int custId, int messageType)
        {
            UserId = userId;
            CustId = custId;
            MessageType = Constants.MessageType.ToString(messageType);
        }

        public int UserId { get; }

        public int CustId { get; }

        public string MessageType { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "Age_MsgCustReadStatus_Upd";

        public override bool IsValid() => CustId > 0;
    }
}
