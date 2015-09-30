using System;

namespace Reference.Common.Contracts.Data
{
    public interface ITimestamp
    {
        DateTimeOffset? Opprettet { get; set; }
        DateTimeOffset? Endret { get; set; }
    }
}