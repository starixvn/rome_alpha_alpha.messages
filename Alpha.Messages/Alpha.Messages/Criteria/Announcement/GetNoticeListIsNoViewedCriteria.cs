namespace Alpha.Messages.Criteria.Announcement
{
    using Fanex.Data.Repository;

    public class GetNoticeListIsNoViewedCriteria : CriteriaBase
    {
        public GetNoticeListIsNoViewedCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_MessageNoticeSelByCustID";

        public override bool IsValid() => CustId > 0;
    }
}
