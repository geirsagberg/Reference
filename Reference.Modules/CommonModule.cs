using Reference.Common.Contracts;
using Reference.Common.Utils;
using Reference.Modules.LightInject;

namespace Reference.Modules
{
    internal class CommonModule : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            // Ensure injected ILog instances are automatically configured with the context of the class they are injected into.
            serviceRegistry.RegisterConstructorDependency<ILog>((factory, info) => new SeriLogger(info.Member.DeclaringType));
        }
    }
}