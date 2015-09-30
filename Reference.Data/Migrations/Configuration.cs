using System.Data.Entity.Migrations;

namespace Reference.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<EntityContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    }
}