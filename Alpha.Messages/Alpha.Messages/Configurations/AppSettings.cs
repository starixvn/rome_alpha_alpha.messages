namespace Alpha.Messages.Configurations
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;

    public static class AppSettings
    {
        [ThreadStatic]
        private static NameValueCollection appSettingValues;

        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "Reviewed")]
        internal static NameValueCollection AppSettingValues
        {
            get { return appSettingValues ?? ConfigurationManager.AppSettings; }

            set { appSettingValues = value; }
        }

        public static bool LocalTest => GetConfigValue("LocalTest") == "1";

        public static string SiteGroup => GetConfigValue("SiteGroup");

        public static string Environment => GetConfigValue("Environment");

        public static string StoreProcedurePath => GetConfigValue("StoredProcedurePath");

        public static string MessageSuper => string.IsNullOrWhiteSpace(GetConfigValue("Message:Super")) ? "3000,3100" : GetConfigValue("Message:Super");

        public static string MessageMaster => string.IsNullOrWhiteSpace(GetConfigValue("Message:Master")) ? "3000,3200" : GetConfigValue("Message:Master");

        public static string MessageAgent => string.IsNullOrWhiteSpace(GetConfigValue("Message:Agent")) ? "3000,3300" : GetConfigValue("Message:Agent");

        public static string RemoteRefURL => GetConfigValue("RemoteRefURL");

        public static string GetConfigValue(string configName) => AppSettingValues[configName];

        public static bool EnableLogInfo => GetConfigValue("EnableLogInfo") == "1";

        public static bool IsDisabledRecheckin => GetConfigValue("DisabledCheckDuplicationLogin") == "1";

        public static void Verify()
        {
            Require(StoreProcedurePath, nameof(StoreProcedurePath));
            Require(Environment, nameof(Environment));
        }

        private static void Require(string key, string keyName)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ConfigurationErrorsException($"{keyName} is null or whitespace");
            }
        }
    }
}