namespace Alpha.Messages.Criteria.Announcement
{
    using Fanex.Data.Repository;

    public class UpdateLastSeenAnnouncementCriteria : NonQueryCommand
    {
        public UpdateLastSeenAnnouncementCriteria(int custId, int messageType, int subAccId)
        {
            CustId = custId;
            SubAccId = subAccId;
            MessageType = Constants.MessageType.ToString(messageType);
        }

        public int SubAccId { get; }

        public int CustId { get; }

        public string MessageType { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "Age_MsgCustReadStatus_Upd";

        public override bool IsValid() => CustId > 0 && !string.IsNullOrEmpty(MessageType);
    }
}
