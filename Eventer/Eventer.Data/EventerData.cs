namespace Eventer.Data
{
    using System;
    using System.Collections.Generic;

    using Eventer.Contracts;
    using Eventer.Data.Repositories;
    using Eventer.Models;

    public class EventerData : IEventerData
    {
        private readonly IEventerDbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public EventerData(IEventerDbContext context)
        {
            this.context = context;
        }

        public IEventerDbContext Context
        {
            get { return this.context; }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public IRepository<Event> Events
        {
            get { return this.GetRepository<Event>(); }
        }

        public IRepository<Category> Categories
        {
            get { return this.GetRepository<Category>(); }
        }

        public IRepository<Tag> Tags
        {
            get { return this.GetRepository<Tag>(); }
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && context != null)
            {
                context.Dispose();
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                repositories.Add(typeof(T), Activator.CreateInstance(type, context));
            }

            return (IRepository<T>)repositories[typeof(T)];
        }
    }
}