using System;

namespace Reference.Common.Contracts.Data
{
    public interface ITransaction : IDisposable
    {
        void Complete();
    }
}