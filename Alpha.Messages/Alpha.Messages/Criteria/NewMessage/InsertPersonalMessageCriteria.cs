namespace Alpha.Messages.Criteria.NewMessage
{
    using System.Globalization;
    using Constants;
    using Fanex.Data;
    using Fanex.Data.Repository;
    using Models.Announcement;

    public class InsertPersonalMessageCriteria : CriteriaBase
    {
        private readonly ResourceManager resourceManager;

        public InsertPersonalMessageCriteria(string resourceKey, ResourceManager resource)
        {
            this.resourceManager = resource;
            ContentEN = GetMessageByCulture(resourceKey, Language.English);
            ContentCH = GetMessageByCulture(resourceKey, Language.ChineseTraditional);
            ContentCS = GetMessageByCulture(resourceKey, Language.ChineseSimplified);
            ContentJP = GetMessageByCulture(resourceKey, Language.Japanese);
            ContentTH = GetMessageByCulture(resourceKey, Language.Thai);
            ContentKO = GetMessageByCulture(resourceKey, Language.Korean);
            MessageType = Constants.MessageType.Personal;
            CreatorName = string.Empty;
            IsBatch = true;
        }
       
        [SpParam(SqlParamName = "content_en")]
        public string ContentEN { get; set; }

        [SpParam(SqlParamName = "content_ch")]
        public string ContentCH { get; set; }

        [SpParam(SqlParamName = "content_cs")]
        public string ContentCS { get; set; }

        [SpParam(SqlParamName = "content_jp")]
        public string ContentJP { get; set; }

        [SpParam(SqlParamName = "content_it")]
        public string ContentIT => string.Empty;

        [SpParam(SqlParamName = "content_th")]
        public string ContentTH { get; set; }

        [SpParam(SqlParamName = "content_ko")]
        public string ContentKO { get; set; }

        [SpParam(SqlParamName = "content_es")]
        public string ContentES => string.Empty;

        [SpParam(SqlParamName = "msgtype")]
        public int MessageType { get; set; }
        
        public string CreatorName { get; }
        
        public bool IsBatch { get; }

        public override string GetSettingKey() => "Dot_Msg_SetMessage";

        public override bool IsValid() => true;

        private string GetMessageByCulture(string resourceKey, string culture)
            => resourceManager.GetString(resourceKey, new CultureInfo(culture)).Replace("<br>", " ");
    }
}
