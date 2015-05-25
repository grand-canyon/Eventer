namespace Eventer.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Eventer.Contracts;
    using Eventer.Models;

    using Microsoft.AspNet.Identity;

    public sealed class Configuration : DbMigrationsConfiguration<EventerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; // TODO: Remove in production
        }

        protected override void Seed(EventerDbContext context)
        {
            SeedCategories(context);
            SeedTags(context);
            SeedEvents(context);
            SeedUsers(context);
        }

        private static void SeedCategories(IEventerDbContext context)
        {
            context.Categories.AddOrUpdate(x => x.Name,
                new Category
                {
                    Name = "Concerts"
                }
            );
        }

        private static void SeedTags(IEventerDbContext context)
        {
            context.Tags.AddOrUpdate(x => x.Name,
                new Tag
                {
                    Name = "Event"
                });
        }

        private static void SeedEvents(IEventerDbContext context)
        {
            var events = new List<Event>
            {
                new Event
                {
                    Title = "Tribe Ibiza",
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Concerts"),
                    Date = new DateTime(2015, 6, 3),
                    Duration = new TimeSpan(0, 3, 30, 0),
                    Description = "Tribe Ibiza Launch Competition",
                    IsActive = true,
                    Location = "Ibiza Old Town in Ciudad de Ibiza, Spain",
                    Status = EventStatus.Open,
                    Tags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.Name == "Event")
                    }
                }
            };

            foreach (var e in events.Where(e => !context.Events.Any(x => x.Title == e.Title)))
            {
                context.Events.AddOrUpdate(e);
            }
        }

        private static void SeedUsers(EventerDbContext context)
        {
            var password = new PasswordHasher().HashPassword("Pass123!");

            context.Users.AddOrUpdate(x => x.Email,
                new User
                {
                    Email = "admin@eventer.com",
                    UserName = "admin",
                    PasswordHash = password,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Events = new List<Event>
                    {
                        context.Events.FirstOrDefault(e => e.Title == "Tribe Ibiza")
                    }
                }
            );
        }
    }
}