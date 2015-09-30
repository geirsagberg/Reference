using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Reflection;
using Reference.Data.Migrations;

namespace Reference.Data
{
    public static class EntryPoint
    {
        private static string GetConnectionString(DbMigrator migrator)
        {
            var props = typeof (DbMigrator).GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            return props.First(p => p.Name == "TargetDatabase").GetValue(migrator).ToString();
        }

        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine(@"Migrating database...");
            var migrator = new DbMigrator(new Configuration());
            Console.WriteLine(@"Connection: " + GetConnectionString(migrator));

            var migrations = migrator.GetPendingMigrations().ToList();

            if (!migrations.Any()) {
                Console.WriteLine(@"No pending migrations.");
            } else {
                foreach (var migration in migrations) {
                    Console.WriteLine(migration);
                    migrator.Update(migration);
                }
            }

            if (args.Contains("--seed")) {
                Console.WriteLine(@"Seeding the database...");
                var context = new EntityContext();

                DatabaseSeeder.Seed(context);

                context.SaveChanges();
            } else {
                Console.WriteLine(@"No seeding required.");
            }
            Console.WriteLine(@"Migration done.");
        }
    }
}