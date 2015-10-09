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

        private static void RegisterTypes(IServiceRegistry container)
        {
            // Registers all ICompostionRoot implementations in this assembly
            container.RegisterAssembly(Assembly.GetExecutingAssembly());
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

        public static void RegisterSingleton<T1, T2>() where T2 : T1 => Container.Register<T1, T2>(new PerContainerLifetime());
        public static void RegisterSingleton<T>() => Container.Register<T>(new PerContainerLifetime());
        public static void RegisterTransient<T1, T2>() where T2 : T1 => Container.Register<T1, T2>();
        public static void RegisterTransient<T1>() => Container.Register<T1>();
        public static void RegisterScoped<T1, T2>() where T2 : T1 => Container.Register<T1, T2>(new PerScopeLifetime());
        public static void RegisterScoped<T1>() => Container.Register<T1>(new PerScopeLifetime());
        public static void RegisterInstance<T>(object instance) => Container.RegisterInstance(typeof (T), instance);

        public static T GetInstance<T>() => Container.GetInstance<T>();
    }
}