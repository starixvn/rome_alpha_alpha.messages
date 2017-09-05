using Fanex.Data.Repository;

namespace Alpha.Messages.Criteria.Message
{
    public class GetMoveBetInformationCriteria : CriteriaBase
    {
        public override string GetSettingKey() => "Age_MoveBetsProcessNotification";

        public override bool IsValid() => true;
    }
}
