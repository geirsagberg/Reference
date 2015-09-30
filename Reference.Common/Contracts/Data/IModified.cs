using System;

namespace Reference.Common.Contracts.Data
{
    public interface IModified
    {
        DateTimeOffset? Endret { get; set; }
    }
}