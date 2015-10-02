using System;
using System.Collections.Generic;
using Reference.Common.Contracts.Data;
using Reference.Domain.Attributes;

namespace Reference.Domain.Entities
{
    [NoPlural]
    public class Kj�p : EntityBase, ITimestamp
    {
        public DateTimeOffset? Opprettet { get; set; }
        public DateTimeOffset? Endret { get; set; }
        public virtual Person Person { get; set; }
        public virtual ISet<VareLinje> VareLinjer { get; set; }
    }
}