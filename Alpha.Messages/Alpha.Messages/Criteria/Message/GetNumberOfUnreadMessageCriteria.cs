namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetNumberOfUnreadMessageCriteria : CriteriaBase
    {
        public GetNumberOfUnreadMessageCriteria(int custId, int subAccId, string parentSites, string site)
        {
            CustId = custId;
            SubAccId = subAccId;
            ParentSites = parentSites;
            Site = site;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        [SpParam(SqlParamName = "targetId2")]
        public string ParentSites { get; }

        public string Site { get; }

        public override string GetSettingKey() => "Age_MsgCustReadStatus_SelUnread";

        public override bool IsValid() => CustId >= 0;
    }
}
