namespace Eventer.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    public interface IEventerDbContext
    {
        // populate interface with db sets
        IDbSet<T> Set<T>() where T : class;
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        int SaveChanges();
    }
}
