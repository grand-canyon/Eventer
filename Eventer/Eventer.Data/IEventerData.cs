namespace Eventer.Data
{
    using Eventer.Data.Repositories;
    using Eventer.Models;

    public interface IEventerData
    {
        IEventerDbContext Context { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}