namespace Alpha.Messages.Api
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Alpha.Messages.Api.Helpers;
    using AlphaPlus.Logging;
    using AlphaPlus.Logging.Web;
    using App_Start;
    using Configurations;
    using Fanex.Data;
    using Fanex.Data.CrossCutting.WrappingException;
    using Fanex.Data.OldConnectionStringProvider;
    using Fanex.Logging.Extensions.Database;
    using Fanex.Logging.Extensions.Web;

    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AlphaPlus.Configurations.ConfigurationManager.Init(new ConfigurationHelper());
            LoggingConfig.ExceptionHandler = new ExceptionHandlerAdapter();
            LoggingConfig.Init(getDbExceptionInfo: DatabaseInfoGetters.GetDbExceptionInfo);

            DependencyConfig.Init();
            AppSettings.Verify();

            DbSettingProviderManager
                .StartNewSession()
                .Use(new OldConnectionStringProvider())
                .UseDefaultDbSettingProvider(resourcePath: AppSettings.StoreProcedurePath, enableWatching: true)
                .Use((settingKey, commandTimeout) => new WrappingExceptionObjectDb(settingKey, commandTimeout))
                .Run();
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