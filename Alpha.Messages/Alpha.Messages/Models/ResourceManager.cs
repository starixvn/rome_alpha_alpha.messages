namespace Alpha.Messages.Models
{
    using Alpha.Messages.AppResources;
    using System.Globalization;

    public interface ResourceManager
    {
        string GetString(string name, CultureInfo culture);
    }

    public class SiteResourceManager : ResourceManager
    {
        public string GetString(string name, CultureInfo culture)
            => Resources.ResourceManager.GetString(name, culture);
    }
}
