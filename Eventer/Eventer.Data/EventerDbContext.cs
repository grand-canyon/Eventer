namespace Eventer.Data
{
    using System.Data.Entity;

    using Eventer.Contracts;
    using Eventer.Data.Migrations;
    using Eventer.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class EventerDbContext : IdentityDbContext<User>, IEventerDbContext
    {
        public const string SqlConnectionString = "Server=.;Database=Eventer;Integrated Security=True;";

        public EventerDbContext()
            : base("DefaultConnection")
        {
        }

        public EventerDbContext(string connectionString = SqlConnectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EventerDbContext, Configuration>());
        }

        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public DbContext DbContext { get { return this; } }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static EventerDbContext Create()
        {
            return new EventerDbContext();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}