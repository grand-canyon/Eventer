namespace Eventer.Data
{
    using System.Data.Entity;

    using Eventer.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class EventerDbContext : IdentityDbContext<User>, IEventerDbContext
    {
        public const string SqlConnectionString = "Server=.;Database=Twitter;Integrated Security=True;";

        public EventerDbContext(string connectionString = SqlConnectionString)
            : base(connectionString)
        {
            // Database.SetInitializer(new MigrateDatabaseToLatestVersion<EventerDbContext, Configuration>());
        }

        public EventerDbContext()
            : base("DefaultConnection")
        {
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}