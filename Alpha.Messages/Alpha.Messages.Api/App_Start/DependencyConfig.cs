namespace Alpha.Messages.Api.App_Start
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Http;
    using Announcements.Services;
    using Client;
    using Controllers;
    using Fanex.Data.Repository;
    using MediatR;
    using Services;
    using SimpleInjector;
    using SimpleInjector.Extensions.ExecutionContextScoping;
    using SimpleInjector.Integration.WebApi;

    public class DependencyConfig
    {
        public static void Init()
        {
            var container = new Container();

            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            RegisterMediator(container);
            RegisterDataAccesses(container);
            RegisterServices(container);
            RegisterAnnouncementService(container);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);

            container.Verify();

            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
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
            container.RegisterSingleton<AnnouncementFormatService, AnnouncementFormatServiceImpl>();
            container.RegisterSingleton<AnnouncementService, AnnouncementServiceImpl>();
            container.RegisterSingleton<CustomerService, CustomerServiceImpl>();

            container.Register<ProductService>(
                () => new ProductServiceImpl(
                    container.GetInstance<IDynamicRepository>(),
                    (_) => true),
                Lifestyle.Singleton);
        }

        private static void RegisterAnnouncementService(Container container)
        {
            container.RegisterSingleton<SpecificationService, SpecificationServiceImpl>();
            container.Register<AnnouncementClient>(() => new AnnouncementClientImpl(), Lifestyle.Singleton);
        }
    }
}