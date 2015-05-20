namespace Eventer.Data
{
    using System.Data.Entity;
    class EventerDbContext : DbContext, IEventerDbContext
    {
        public EventerDbContext()
            : base("Eventer")
        {
        }

        public IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
