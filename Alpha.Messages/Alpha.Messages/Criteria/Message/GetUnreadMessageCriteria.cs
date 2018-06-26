namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data.Repository;

    public class GetUnreadMessageCriteria : CriteriaBase
    {
        public GetUnreadMessageCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_MsgCustReadStatus_SelUnread";

        public override bool IsValid() => CustId >= 0;
    }
}
