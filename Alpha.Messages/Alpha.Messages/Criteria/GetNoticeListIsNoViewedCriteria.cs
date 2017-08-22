namespace Alpha.Messages.Criteria
{
    using Fanex.Data.Repository;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

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
