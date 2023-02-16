using Fanex.Data.Repository;

namespace Alpha.Messages.Criteria.Feedback
{
    public class CheckExistsCriteria : NonQueryCommand
    {
        public CheckExistsCriteria(int custId, int subAccId)
        {
            CustId = custId;
            SubAccId = subAccId;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsExists { get; set; }

        public override string GetSettingKey()
        {
            return "Age_CustFeedbackCheckExists";
        }

        public override bool IsValid() => CustId > 0;
    }
}
