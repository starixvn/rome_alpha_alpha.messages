namespace Alpha.Messages.Criteria.NewMessage
{
    using Fanex.Data.Repository;

    public class UpdateMessageAsReadOnUserCritria : CriteriaBase
    {
        public UpdateMessageAsReadOnUserCritria(int custId, int typeId)
        {
            CustId = custId;
            TypeId = typeId;
            IsViewed = true;
        }

        public int CustId { get; }

        public bool IsViewed { get; }

        public int TypeId { get; }

        public override string GetSettingKey() => "Age_MessageNoticeUpdStatus";

        public override bool IsValid() => CustId > 0;
    }
}
