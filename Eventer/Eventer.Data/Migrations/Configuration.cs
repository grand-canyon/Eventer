namespace Eventer.Data.Migrations
{
    using System.Data.Entity.Migrations;

    using Eventer.Contracts;
    using Eventer.Models;

    public sealed class Configuration : DbMigrationsConfiguration<EventerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; // TODO: Remove in production
        }

        protected override void Seed(EventerDbContext context)
        {
            // TODO: SeedUsers(context)
            // TODO: SeedEvents(context)
        }


        private static void SeedUsers(IEventerDbContext context)
        {
            context.Users.AddOrUpdate(
                new User()
                {

                }
            );
        }

        private static void SeedEvents(IEventerDbContext context)
        {
            context.Events.AddOrUpdate(
                new Event()
                {

                }
            );
        }
    }
}