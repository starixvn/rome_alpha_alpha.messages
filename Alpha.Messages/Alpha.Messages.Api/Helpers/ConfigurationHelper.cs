using System.ComponentModel;
using System;
using AlphaPlus.Configurations;

namespace Alpha.Messages.Api.Helpers
{
    public class ConfigurationHelper : IConfigurationHelper
    {
        public T GetValue<T>(string key, T defaultValue)
        {
            try
            {
                var value = System.Configuration.ConfigurationManager.AppSettings[key];
                if (value == null)
                {
                    return defaultValue;
                }

                TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));

                return (T)typeConverter.ConvertFromString(value);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Key: {key}", ex);
            }
        }
    }
}