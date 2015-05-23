namespace Eventer.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Eventer.Models;

    public interface IEventerDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        void Dispose();
    }
}