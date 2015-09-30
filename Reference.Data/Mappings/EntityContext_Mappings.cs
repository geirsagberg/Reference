using System.Data.Entity.ModelConfiguration;
using Reference.Domain.Entities;

namespace Reference.Data
{
    internal partial class EntityContext
    {
        protected override void MapPerson(EntityTypeConfiguration<Person> config)
        {
            base.MapPerson(config);

            config.HasMany(p => p.Kjøp).WithRequired(k => k.Person);
        }
    }
}