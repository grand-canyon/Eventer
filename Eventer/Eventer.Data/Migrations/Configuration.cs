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
            SeedEvents(context);
            SeedUsers(context);
        }

        private static void SeedCategories(IEventerDbContext context)
        {
            context.Categories.AddOrUpdate(x => x.Name,
                new Category
                {
                    Name = "Festival"
                }
            );
        }

        private static void SeedEvents(IEventerDbContext context)
        {
            var events = new List<Event>
            {
                new Event
                {
                    Title = "Tribe Ibiza",
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Festival"),
                    Date = new DateTime(2015, 6, 3),
                    Description = "Tribe Ibiza Launch Competition",
                    IsActive = true,
                    Location = "Ibiza Old Town in Ciudad de Ibiza, Spain",
                    Status = EventStatus.Open
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
                    Email = "klaxon@abv.bg",
                    UserName = "Karim",
                    PasswordHash = password,
                    Events = new List<Event>
                    {
                        context.Events.FirstOrDefault(e => e.Title == "Tribe Ibiza")
                    }
                }
            );
        }
    }
}