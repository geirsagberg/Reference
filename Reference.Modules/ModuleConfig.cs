using System;
using System.Reflection;
using System.Web.Http;
using Reference.Modules.LightInject;

namespace Reference.Modules
{
    public class ModuleConfig
    {
        private static readonly Lazy<ServiceContainer> _container = new Lazy<ServiceContainer>(() => {
            var container = new ServiceContainer();
            RegisterTypes(container);
            return container;
        });

        private static ServiceContainer Container => _container.Value;

        private static void RegisterTypes(ServiceContainer container)
        {
            // Registers all ICompostionRoot implementations in this assembly
            container.AssemblyScanner.Scan(Assembly.GetExecutingAssembly(), container);
        }

        public static void EnableMvc()
        {
            Container.RegisterControllers(Assembly.GetCallingAssembly());
            Container.EnableMvc();
        }

        public static void EnableWebApi(HttpConfiguration config)
        {
            Container.RegisterApiControllers(Assembly.GetCallingAssembly());
            Container.EnablePerWebRequestScope();
            Container.EnableWebApi(config);
        }

        public static void RegisterPerContainerLifetime<T1, T2>()
        {
            Container.Register(typeof (T1), typeof (T2), new PerContainerLifetime());
        }

        public static void RegisterPerContainerLifetime<T>()
        {
            Container.Register(typeof (T), new PerContainerLifetime());
        }

        public static T GetInstance<T>() => Container.GetInstance<T>();

        public static void RegisterInstance<T>(object instance)
        {
            Container.RegisterInstance(typeof (T), instance);
        }
    }
}