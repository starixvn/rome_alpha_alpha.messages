namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data.Repository;

    public class GetMoveBetInformationCriteria : CriteriaBase
    {
        public GetMoveBetInformationCriteria()
        {
            TimeZone = 8;
        }

        public int TimeZone { get; }

        public override string GetSettingKey() => "Age_MoveBetsProcessNotification";

        public override bool IsValid() => true;
    }
}
