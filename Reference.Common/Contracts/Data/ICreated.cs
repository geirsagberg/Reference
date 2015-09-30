using System;

namespace Reference.Common.Contracts.Data
{
    public interface ICreated
    {
        DateTimeOffset? Opprettet { get; set; }
    }
}