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
    using Fanex.Data.SqlServer;
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
                .WithSqlServer(resourcePath: AppSettings.StoreProcedurePath)
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