using System;

namespace Reference.Common.Contracts.Data
{
    public interface ITimestamp : ICreated, IModified {}

    public interface ICreated
    {
        DateTimeOffset? Opprettet { get; set; }
    }

    public interface IModified
    {
        DateTimeOffset? Endret { get; set; }
    }
}