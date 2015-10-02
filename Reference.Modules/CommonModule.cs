using Reference.Common.Contracts;
using Reference.Common.Utils;
using Reference.Modules.LightInject;

namespace Reference.Modules
{
    internal class CommonModule : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterConstructorDependency<ILog>((factory, info) => new SeriLogger(info.Member.DeclaringType));
        }
    }
}