namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data.Repository;

    public class GetMoveBetInformationCriteria : CriteriaBase
    {
        private const int ChinaGTM = 8;

        public int TimeZone { get; } = ChinaGTM;

        public override string GetSettingKey() => "Age_MoveBetsProcessNotification";

        public override bool IsValid() => true;
    }
}
