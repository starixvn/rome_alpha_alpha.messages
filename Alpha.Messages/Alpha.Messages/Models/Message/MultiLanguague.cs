namespace Alpha.Messages.Models.Message
{
    using Alpha.Messages.Constants;

    public class MultiLanguague
    {
        public MultiLanguague(
            string contentEN,
            string contentCS,
            string contentCH,
            string contentJP,
            string contentTH,
            string contentKO,
            string contentVN)
        {
            ContentEN = string.IsNullOrEmpty(contentEN) ? string.Empty : contentEN;
            ContentCS = string.IsNullOrEmpty(contentCS) ? string.Empty : contentCS;
            ContentCH = string.IsNullOrEmpty(contentCH) ? string.Empty : contentCH;
            ContentJP = string.IsNullOrEmpty(contentJP) ? string.Empty : contentJP;
            ContentTH = string.IsNullOrEmpty(contentTH) ? string.Empty : contentTH;
            ContentKO = string.IsNullOrEmpty(contentKO) ? string.Empty : contentKO;
            ContentVN = string.IsNullOrEmpty(contentVN) ? string.Empty : contentVN;
        }

        public string ContentEN { get; internal set; }

        public string ContentCS { get; internal set; }

        public string ContentCH { get; internal set; }

        public string ContentJP { get; internal set; }

        public string ContentTH { get; internal set; }

        public string ContentKO { get; internal set; }

        public string ContentVN { get; internal set; }

        public string GetContentByKey(string languageKey)
        {
            var content = string.Empty;
            switch (languageKey)
            {
                case Language.Thai:
                    content = ContentTH;
                    break;

                case Language.ChineseSimplified:
                    content = ContentCS;
                    break;

                case Language.ChineseTraditional:
                    content = string.IsNullOrEmpty(ContentCH) ? ContentCS : ContentCH;
                    break;

                case Language.Japanese:
                    content = ContentJP;
                    break;

                case Language.Korean:
                    content = ContentKO;
                    break;

                case Language.Vietnamese:
                    content = ContentVN;
                    break;

                default:
                    content = ContentEN;
                    break;
            }

            return string.IsNullOrEmpty(content) ? ContentEN : content;
        }
    }
}