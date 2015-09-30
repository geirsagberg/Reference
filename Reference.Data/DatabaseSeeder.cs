using Reference.Domain.Entities;

namespace Reference.Data
{
    internal static class DatabaseSeeder
    {
        internal static void Seed(EntityContext context)
        {
            context.AddOrUpdate(new Person {
                Id = 1,
                Fornavn = "Geir",
                Etternavn = "Sagberg"
            });
        }
    }
}