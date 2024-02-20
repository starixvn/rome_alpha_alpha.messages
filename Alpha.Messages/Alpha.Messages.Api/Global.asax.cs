namespace Alpha.Messages.Api
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using Alpha.Messages.Api.Helpers;
    using AlphaPlus.Logging;
    using AlphaPlus.Logging.Web;
    using App_Start;
    using Configurations;
    using Fanex.Data;
    using Fanex.Logging.Extensions.Web;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var connectionStrings = ConfigurationManager.ConnectionStrings
                .Cast<ConnectionStringSettings>()
                .ToDictionary(
                connection => connection.Name,
                connection => new ConnectionConfiguration(connection.Name, connection.ConnectionString, connection.ProviderName),
                StringComparer.OrdinalIgnoreCase);
            DbSettingProviderManager
                .StartNewSession()
                .Use(connectionStrings)
                .Use(AppSettings.FanexDataServiceApi)
                .Register(resourcePath: AppSettings.StoreProcedurePath)
                .Run();

            AlphaPlus.Configurations.ConfigurationManager.Init(new ConfigurationHelper());
            LoggingConfig.ExceptionHandler = new ExceptionHandlerAdapter();
            LoggingConfig.Init();
            DependencyConfig.Init();
            AppSettings.Verify();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            if (Request.IsLocal)
            {
                return;
            }

            Exception ex = Server.GetLastError();

            ExceptionHandler.Handle(ex);
        }
    }
}