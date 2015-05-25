namespace Eventer.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Eventer.Contracts;
    using Eventer.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

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
            SeedRoles(context);
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

        private static void SeedRoles(EventerDbContext context)
        {
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roles = new List<IdentityRole>
            {
                new IdentityRole("Admin"),
                new IdentityRole("User"),
                new IdentityRole("Guest")
            };

            foreach (var role in roles)
            {
                if (roleManager.FindByName(role.Name) == null)
                {
                    roleManager.Create(role);
                }
            }

            roleStore.Context.SaveChanges();
        }

        private static void SeedUsers(EventerDbContext context)
        {
            var userStore = new UserStore<User>(context);
            var userManager = new UserManager<User>(userStore);

            var users = new List<User>
            {
                new User
                {
                    Email = "admin@eventer.com",
                    UserName = "admin",
                    LockoutEnabled = true,
                    Events = new List<Event>
                    {
                        context.Events.FirstOrDefault(e => e.Title == "Tribe Ibiza")
                    }
                }
            };

            foreach (var user in users)
            {
                if (userManager.FindByName(user.UserName) == null)
                {
                    userManager.Create(user, "Pass123!");
                    userManager.SetLockoutEnabled(user.Id, false);
                    userManager.AddToRole(user.Id, "User");
                }
            
            }

            userManager.AddToRole(users.FirstOrDefault(x => x.UserName == "admin").Id, "Admin");

            userStore.Context.SaveChanges();
        }
    }
}