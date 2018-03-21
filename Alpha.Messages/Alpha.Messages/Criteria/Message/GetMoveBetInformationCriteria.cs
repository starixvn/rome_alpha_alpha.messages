namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data.Repository;

    public class GetMoveBetInformationCriteria : CriteriaBase
    {
        public int TimeZone { get; set; } = 8;

        public override string GetSettingKey() => "Age_MoveBetsProcessNotification";

        public override bool IsValid() => true;
    }
}
