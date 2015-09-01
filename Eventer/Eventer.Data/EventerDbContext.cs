namespace Eventer.Data
{
    using System.Data.Entity;

    using Eventer.Common;
    using Eventer.Contracts;
    using Eventer.Data.Migrations;
    using Eventer.Models;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class EventerDbContext : IdentityDbContext<User>, IEventerDbContext
    {
        public EventerDbContext()
            : base(GlobalConstants.RemoteConnectionString, false)
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

        public static EventerDbContext Create()
        {
            return new EventerDbContext();
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