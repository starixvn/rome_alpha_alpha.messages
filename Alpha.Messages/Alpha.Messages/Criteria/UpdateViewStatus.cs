using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Criteria
{
    public class UpdateViewStatus : CriteriaBase
    {
        public UpdateViewStatus(int custId, bool isViewed, int typeId)
        {
            CustId = custId;
            IsViewed = isViewed;
            TypeId = typeId;
        }

        public int CustId { get; }

        public bool IsViewed { get; }

        public int TypeId { get; }

        public override string GetSettingKey() => "Age_MessageNoticeUpdStatus";

        public override bool IsValid() => CustId > 0;
    }
}
