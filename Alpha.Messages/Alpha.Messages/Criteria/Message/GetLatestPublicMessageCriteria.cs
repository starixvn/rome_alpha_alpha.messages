namespace Alpha.Messages.Criteria.Message
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class GetLatestPublicMessageCriteria : CriteriaBase
    {
        public GetLatestPublicMessageCriteria(int messageType, string site, bool isTest)
        {
            MessageType = messageType;
            IsTest = isTest;

            Site = site;
        }

        private string Site { get; set; }

        public int MessageType { get; }

        public string Site1 => !string.IsNullOrEmpty(Site) ? Site.Split(',')[0] : string.Empty;

        public string Site2
        {
            get
            {
                var site2 = string.Empty;
                if (!string.IsNullOrEmpty(Site))
                {
                    var sites = Site.Split(',');
                    site2 = sites.Length > 1 ? sites[1] : string.Empty;
                }

                return site2;
            }
        }

        public string Site3
        {
            get
            {
                var site3 = string.Empty;
                if (!string.IsNullOrEmpty(Site))
                {
                    var sites = Site.Split(',');
                    site3 = sites.Length > 2 ? sites[2] : string.Empty;
                }

                return site3;
            }
        }

        [SpParam(SqlParamName = "latestmsgid")]
        public int LatestMessageId => 0;

        public bool IsTest { get; }

        public override string GetSettingKey() => "Acc_Msg_GetTopPubMessage";

        public override bool IsValid() => !string.IsNullOrEmpty(Site1) && MessageType >= 0;
    }
}
