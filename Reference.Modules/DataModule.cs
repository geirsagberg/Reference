﻿using Reference.Data;
using Reference.Modules.LightInject;

namespace Reference.Modules
{
    internal class DataModule : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.RegisterAssembly(typeof (EntityContext).Assembly);
        }
    }
}