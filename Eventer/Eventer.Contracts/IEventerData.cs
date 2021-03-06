﻿namespace Eventer.Contracts
{
    using Eventer.Models;

    public interface IEventerData
    {
        IEventerDbContext Context { get; }

        IRepository<User> Users { get; }

        IRepository<Event> Events { get; }

        IRepository<Category> Categories { get; }

        IRepository<Tag> Tags { get; }

        IRepository<Comment> Comments { get; } 

        int SaveChanges();
    }
}