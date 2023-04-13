namespace Alpha.Messages.Criteria.Feedback
{
    using Alpha.Messages.Params.Feedback;
    using Fanex.Data.Repository;

    public class AddFeedbackCriteria : NonQueryCommand
    {
        private const int MaxCommentLength = 4000;

        public AddFeedbackCriteria(AddFeedbackParam param)
        {
            CustId = param.CustId;
            SubAccId = param.SubAccountId;
            Comment = param.Comment ?? string.Empty;
            Rate = param.Rate;
            FuncId = param.FunctionId;
            XMLfb = param.XMLFeedback;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public string Comment { get; }

        public byte Rate { get; }

        public int? FuncId { get; }

        public string XMLfb { get; }

        public override string GetSettingKey()
            => "Age_CustFeedbackIns";

        public override bool IsValid()
            => Rate != 0 ||
            (!string.IsNullOrWhiteSpace(Comment) && Comment.Length <= MaxCommentLength)
            || !string.IsNullOrWhiteSpace(XMLfb);
    }
}
