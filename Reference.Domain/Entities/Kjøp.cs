using System.Collections.Generic;
using Reference.Domain.Attributes;

namespace Reference.Domain.Entities
{
    [NoPlural]
    public class Kj�p : EntityBase
    {
        public virtual Person Person { get; set; }
        public virtual ISet<Vare> Varer { get; set; }
    }
}