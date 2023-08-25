namespace Alpha.Messages.Site.App_Start
{
    using Alpha.Web.Helpers;
    using AlphaPlus.User.Models;
    using Announcements.Models;
    using Announcements.Services;
    using Client;
    using Controllers;
    using Fanex.Caching;
    using Fanex.Data.Repository;
    using MediatR;
    using Models.Announcement;
    using Requests.Announcement;
    using Services;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Web.Mvc;

    public static class DependencyConfig
    {
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Reviewed")]
        public static void Init()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            RegisterMediator(container);

            RegisterDataAccesses(container);

            RegisterServices(container);

            RegisterAnnouncementService(container);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        private static void RegisterMediator(Container container)
        {
            var assemblies = GetAssemblies();
            container.RegisterSingleton<IMediator, Mediator>();
            container.Register(typeof(IRequestHandler<,>), assemblies);
            container.Register(typeof(IAsyncRequestHandler<,>), assemblies);

            container.RegisterCollection(typeof(INotificationHandler<>), assemblies);
            container.RegisterCollection(typeof(IAsyncNotificationHandler<>), assemblies);
            container.RegisterSingleton(new SingleInstanceFactory(container.GetInstance));
            container.RegisterSingleton(new MultiInstanceFactory(container.GetAllInstances));
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            yield return typeof(AnnouncementController).Assembly;
        }

        private static void RegisterDataAccesses(Container container)
        {
            container.Register<IDynamicRepository, DynamicRepository>(Lifestyle.Singleton);
        }

        private static void RegisterServices(Container container)
        {
            UserSession.Init(new HttpContextHelper());
            container.RegisterSingleton<IUserSession, UserSessionExt>();
            container.RegisterSingleton<ICacheService>(() => new CacheService());
            container.RegisterPerWebRequest<NewMessageService, NewMessageServiceImpl>();
            container.RegisterPerWebRequest<MessageHistoryService, MessageHistoryServiceImpl>();
            container.RegisterPerWebRequest<MarqueeMessageService, MarqueeMessageServiceImpl>();
            container.RegisterPerWebRequest<PortalMessageService, PortalMessageServiceImpl>();
            container.RegisterSingleton<AnnouncementFormatService, AnnouncementFormatServiceImpl>();
            container.RegisterSingleton<AnnouncementService, AnnouncementServiceImpl>();

            container.Register<ProductService>(
                () => new ProductServiceImpl(
                    container.GetInstance<ICacheService>(),
                    container.GetInstance<IDynamicRepository>(),
                    UserPermission.HasPermissionOnProduct),
                Lifestyle.Singleton);
            container.RegisterSingleton<ResourceManager, SiteResourceManager>();
            container.RegisterPerWebRequest<FeedbackService, FeedbackServiceImpl>();
            container.RegisterPerWebRequest<BackupDomainsService, BackupDomainsServiceImpl>();
            container.RegisterSingleton<CustomerService, CustomerServiceImpl>();
            container.RegisterSingleton<ToastMessageService, ToastMessageServiceImpl>();
        }

        private static void RegisterAnnouncementService(Container container)
        {
            container.RegisterSingleton<IParam, SpecificationParameter>();
            container.RegisterSingleton<SpecificationService, SpecificationServiceImpl>();
            container.Register<AnnouncementClient>(() => new AnnouncementClientImpl(), Lifestyle.Singleton);
        }
    }
}