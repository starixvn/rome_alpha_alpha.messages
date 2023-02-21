namespace Alpha.Messages.Params.Feedback
{
    public class AddFeedbackParam
    {
        public AddFeedbackParam(
            int custId,
            int subAccId,
            string comment,
            int rate)
        {
            CustId = custId;
            SubAccountId = subAccId;
            Comment = comment;
            Rate = rate;
        }

        public int CustId { get; }

        public int SubAccountId { get; }

        public string Comment { get; }

        public int Rate { get; }
    }
}
