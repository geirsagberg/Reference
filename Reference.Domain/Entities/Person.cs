using System.Collections.Generic;

namespace Reference.Domain.Entities
{
    public class Person : EntityBase
    {
        public string Fornavn { get; set; }
        public string Etternavn { get; set; }
        public virtual ISet<Kjøp> Kjøp { get; set; }
    }
}