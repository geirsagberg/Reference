using Reference.BusinessLogic.Logic;
using Reference.Modules.LightInject;

namespace Reference.Modules
{
    internal class LogicModule : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterAssembly(typeof (PersonLogic).Assembly);
        }
    }
}