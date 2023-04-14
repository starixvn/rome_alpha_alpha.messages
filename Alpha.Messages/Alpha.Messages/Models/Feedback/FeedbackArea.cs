namespace Alpha.Messages.Models.Feedback
{
    using Alpha.Messages.Constants;

    public class FeedbackArea
    {
        public FeedbackArea()
        {
        }

        public FeedbackArea(
            int areaId,
            string areaNameEN,
            string areaNameCN3,
            string areaNameCS,
            string areaNameCH,
            string areaNameTH,
            string areaNameJP,
            string areaNameKO)
        {
            AreaId = areaId;
            AreaNameEN = areaNameEN;
            AreaNameCN3 = areaNameCN3;
            AreaNameCS = areaNameCS;
            AreaNameCH = areaNameCH;
            AreaNameTH = areaNameTH;
            AreaNameJP = areaNameJP;
            AreaNameKO = areaNameKO;
        }

        public int AreaId { get; }

        public string AreaNameEN { get; }

        public string AreaNameCN3 { get; }

        public string AreaNameCS { get; }

        public string AreaNameCH { get; }

        public string AreaNameTH { get; }

        public string AreaNameJP { get; }

        public string AreaNameKO { get; }

        public string GetAreaName(string language)
        {
            switch (language)
            {
                case Language.ChineseSimplified:
                    return AreaNameCS;

                case Language.ChineseTraditional:
                    return AreaNameCH;

                case Language.Thai:
                    return AreaNameTH;

                case Language.Vietnamese:
                    return AreaNameCN3;

                case Language.Japanese:
                    return string.IsNullOrWhiteSpace(AreaNameJP) ? AreaNameEN : AreaNameJP;

                case Language.Korean:
                    return string.IsNullOrWhiteSpace(AreaNameKO) ? AreaNameEN : AreaNameKO;

                default:
                    return AreaNameEN;
            }
        }
    }
}
