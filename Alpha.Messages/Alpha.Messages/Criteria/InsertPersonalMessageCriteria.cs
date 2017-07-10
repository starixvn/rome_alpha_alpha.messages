using Alpha.Messages.AppResources;
using Fanex.Data;
using Fanex.Data.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alpha.Messages.Criteria
{
    public class InsertPersonalMessageCriteria : CriteriaBase
    {
        public const int PositionTakingChanged = 1;

        private readonly ResourceManager resourceManager;

        public InsertPersonalMessageCriteria(string resourceKey, ResourceManager resource)
        {
            this.resourceManager = resource;
            ContentEN = GetMessageByCulture(resourceKey, "en-US");
            ContentCH = GetMessageByCulture(resourceKey, "zh-TW");
            ContentCS = GetMessageByCulture(resourceKey, "zh-CN");
            ContentJP = GetMessageByCulture(resourceKey, "ja-JP");
            ContentIT = string.Empty;
            ContentTH = GetMessageByCulture(resourceKey, "th-TH");
            ContentKO = GetMessageByCulture(resourceKey, "ko-KR");
            ContentES = string.Empty;
            MessageType = PositionTakingChanged;
            IsSticky = false;
            MatchId = 0;
            IsBatch = true;

            Creator = 0;
            IsTest = false;
            CreatorName = string.Empty;
        }

        public InsertPersonalMessageCriteria(string resourceKey) : this(resourceKey, null)
        {
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
        public string ContentIT { get; set; }

        [SpParam(SqlParamName = "content_th")]
        public string ContentTH { get; set; }

        [SpParam(SqlParamName = "content_ko")]
        public string ContentKO { get; set; }

        [SpParam(SqlParamName = "content_es")]
        public string ContentES { get; set; }

        [SpParam(SqlParamName = "msgtype")]
        public int MessageType { get; set; }

        public bool IsSticky { get; set; }

        public int Creator { get; set; }

        public string CreatorName { get; set; }

        public bool IsTest { get; set; }

        public int MatchId { get; set; }

        public bool IsBatch { get; set; }

        public override string GetSettingKey() => "Dot_Msg_SetMessage";

        public override bool IsValid() => true;

        private string GetMessageByCulture(string resourceKey, string culture)
            => resourceManager.GetString(resourceKey, new CultureInfo(culture)).Replace("<br>", " ");
    }
}
