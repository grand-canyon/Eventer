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
        public const string ConnectionStringAzure = "Server=ibz4rymk74.database.windows.net;Database=Eventer;Persist Security Info=True;User ID=antalya;Password=Parola123;";

        public EventerDbContext()
            : base("DefaultConnection", false)
        {
        }

        public EventerDbContext(string connectionString = SqlConnectionString)
            : base("DefaultConnection", false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EventerDbContext, DbMigrationsConfiguration>());
        }

        public virtual IDbSet<Event> Events { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Tag> Tags { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public DbContext DbContext
        {
            get { return this; }
        }

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