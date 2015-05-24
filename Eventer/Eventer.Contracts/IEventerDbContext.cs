namespace Eventer.Contracts
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Eventer.Models;

    public interface IEventerDbContext
    {
        IDbSet<User> Users { get; set; }

        IDbSet<Event> Events { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();
    }
}