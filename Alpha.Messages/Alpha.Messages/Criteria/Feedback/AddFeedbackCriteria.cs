namespace Alpha.Messages.Criteria.Feedback
{
    using Fanex.Data.Repository;

    public class AddFeedbackCriteria : NonQueryCommand
    {
        private const int MaxCommentLength = 4000;

        public AddFeedbackCriteria(
            int custId,
            int subAccId,
            string comment,
            byte rate)
        {
            CustId = custId;
            SubAccId = subAccId;
            Comment = comment ?? string.Empty;
            Rate = rate;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public string Comment { get; }

        public byte Rate { get; }

        public override string GetSettingKey()
            => "Age_CustFeedbackIns";

        public override bool IsValid()
            => Rate != 0 ||
            (!string.IsNullOrWhiteSpace(Comment) && Comment.Length <= MaxCommentLength);
    }
}
