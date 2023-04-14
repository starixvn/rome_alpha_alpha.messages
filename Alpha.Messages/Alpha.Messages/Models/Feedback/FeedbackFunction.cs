namespace Alpha.Messages.Models.Feedback
{
    using Alpha.Messages.Constants;

    public class FeedbackFunction
    {
        public FeedbackFunction()
        {
        }

        public FeedbackFunction(
            int funcId,
            string funcNameEN,
            string funcNameCN3,
            string funcNameCS,
            string funcNameCH,
            string funcNameTH,
            string funcNameJP,
            string funcNameKO)
        {
            FuncId = funcId;
            FuncNameEN = funcNameEN;
            FuncNameCN3 = funcNameCN3;
            FuncNameCS = funcNameCS;
            FuncNameCH = funcNameCH;
            FuncNameTH = funcNameTH;
            FuncNameJP = funcNameJP;
            FuncNameKO = funcNameKO;
        }

        public int FuncId { get; }

        public string FuncNameEN { get; }

        public string FuncNameCN3 { get; }

        public string FuncNameCS { get; }

        public string FuncNameCH { get; }

        public string FuncNameTH { get; }

        public string FuncNameJP { get; }

        public string FuncNameKO { get; }

        public string GetFunctionName(string language)
        {
            switch (language)
            {
                case Language.ChineseSimplified:
                    return FuncNameCS;

                case Language.ChineseTraditional:
                    return FuncNameCH;

                case Language.Thai:
                    return FuncNameTH;

                case Language.Vietnamese:
                    return FuncNameCN3;

                case Language.Japanese:
                    return string.IsNullOrWhiteSpace(FuncNameJP) ? FuncNameEN : FuncNameJP;

                case Language.Korean:
                    return string.IsNullOrWhiteSpace(FuncNameKO) ? FuncNameEN : FuncNameKO;

                default:
                    return FuncNameEN;
            }
        }
    }
}
