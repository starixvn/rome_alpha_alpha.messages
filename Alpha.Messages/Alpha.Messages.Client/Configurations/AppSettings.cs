namespace Alpha.Messages.Client.Configurations
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public static class AppSettings
    {
        [ThreadStatic]
        private static NameValueCollection appSettingValues;
        
        internal static NameValueCollection AppSettingValues
        {
            get { return appSettingValues ?? ConfigurationManager.AppSettings; }

            set { appSettingValues = value; }
        }

        public static string AnnouncementApi => GetConfigValue("Announcement:ApiUrl");

        public static string AnnouncementSecretKey 
            => string.IsNullOrEmpty(GetConfigValue("Announcement:SecretKey")) ? 
            "ZDlhMWFmMDgtZGQ0ZC00NWY0LTg1ODItNzVkNTdhOTNjOTEx" : 
            GetConfigValue("Announcement:SecretKey");
               
        public static string GetConfigValue(string configName) => AppSettingValues[configName];       
    }
}