namespace Alpha.Messages.Criteria.Announcement
{
    using Fanex.Data.Repository;

    public class GetLastSeenAnnouncementCriteria : CriteriaBase
    {
        public GetLastSeenAnnouncementCriteria(int custId)
        {
            CustId = custId;
        }

        public int CustId { get; }

        public override string GetSettingKey() => "Age_MsgCustReadStatus_Sel";

        public override bool IsValid() => CustId > 0;
    }
}
