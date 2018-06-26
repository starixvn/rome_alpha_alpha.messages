namespace Alpha.Messages.Criteria.NewMessage
{
    using Fanex.Data.Repository;

    public class GetUnreadMessagesOnUserCriteria : CriteriaBase
    {
        public GetUnreadMessagesOnUserCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_MessageNoticeSelByCustID";

        public override bool IsValid() => CustId > 0;
    }
}
