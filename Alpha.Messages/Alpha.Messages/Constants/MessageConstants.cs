namespace Alpha.Messages.Constants
{
    using System.Collections.Generic;
    using Alpha.Messages.Configurations;
    using Alpha.Messages.Models.Constants;

    public static class MessageConstants
    {
        public static readonly Dictionary<byte, string> ParentSitesByLevel = new Dictionary<byte, string>
        {
            [UserLevel.Super] = AppSettings.MessageSuper,
            [UserLevel.Master] = AppSettings.MessageMaster,
            [UserLevel.Agent] = AppSettings.MessageAgent
        };

        public static readonly Dictionary<int, int> SpecialTypes = new Dictionary<int, int>
        {
            [MessageType.Normal] = 0,
            [MessageType.Special] = 1,
            [MessageType.Personal] = 2
        };

        public const string ContentFontStartMarkup = "<font color=red>";
        public const string ContentFontEndMarkup = "</font>";
        public const int DefaultTotalMessageHistory = 100;
        public const string SiteMessageTag = "{site-message}";
        public const string ProductsTag = "{products}";
    }
}
