namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetNumberOfUnreadMessageCriteria : CriteriaBase
    {
        public GetNumberOfUnreadMessageCriteria(int custId, int subAccId, string parentSites)
        {
            CustId = custId;
            SubAccId = subAccId;
            ParentSites = parentSites;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        [SpParam(SqlParamName = "targetId2")]
        public string ParentSites { get; }

        public override string GetSettingKey() => "Age_MsgCustReadStatus_SelUnread";

        public override bool IsValid() => CustId >= 0;
    }
}
