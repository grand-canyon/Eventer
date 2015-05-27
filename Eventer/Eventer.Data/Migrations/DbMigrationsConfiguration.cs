namespace Eventer.Data.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Contracts;
    using Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public sealed class DbMigrationsConfiguration : DbMigrationsConfiguration<EventerDbContext>
    {
        public DbMigrationsConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true; // TODO: Remove in production
        }

        protected override void Seed(EventerDbContext context)
        {
            if (!context.Categories.Any())
            {
                SeedCategories(context);
            }
            if (!context.Tags.Any())
            {
                SeedTags(context);
            }
            if (!context.Events.Any())
            {
                SeedEvents(context);
            }
            if (!context.Roles.Any())
            {
                SeedRoles(context);
            }
            if (!context.Users.Any())
            {
                SeedUsers(context);
            }
        }

        private static void SeedCategories(IEventerDbContext context)
        {
            var categories = new List<Category>
            {
                new Category {Name = "Concerts & Music", UrlSlug = "concerts-music"},
                new Category {Name = "Business & Professional", UrlSlug = "business-professional"},
                new Category {Name = "Science & Technology", UrlSlug = "science-technology"},
                new Category {Name = "Performing & Visual Arts", UrlSlug = "performing-visual-arts"},
                new Category {Name = "Travel & Outdoor", UrlSlug = "travel-outdoor"},
                new Category {Name = "Sport & Fitness", UrlSlug = "sport-fitness"},
                new Category {Name = "Other", UrlSlug = "other"},
            };

            foreach (var category in categories)
            {
                context.Categories.Add(category);
            }

            context.SaveChanges();
        }

        private static void SeedTags(IEventerDbContext context)
        {
            var tags = new List<Tag>
            {
                new Tag {Name = "Free", UrlSlug = "free"},
                new Tag {Name = "Experience", UrlSlug = "experience"},
                new Tag {Name = "Jobs", UrlSlug = "jobs"},
                new Tag {Name = "Programming", UrlSlug = "programming"},
                new Tag {Name = "Android", UrlSlug = "android"},
                new Tag {Name = "Development", UrlSlug = "development"},
                new Tag {Name = "Software", UrlSlug = "software"},
                new Tag {Name = "Event", UrlSlug = "event"}
            };

            foreach (var tag in tags)
            {
                context.Tags.Add(tag);
            }

            context.SaveChanges();  
        }

        private static void SeedEvents(IEventerDbContext context)
        {
            var events = new List<Event>
            {
                new Event
                {
                    Title = "Tribe Ibiza",
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Concerts & Music"),
                    Date = new DateTime(2015, 6, 3),
                    Duration = new TimeSpan(0, 3, 30, 0),
                    Description = "Tribe Ibiza Launch Competition",
                    IsActive = true,
                    Location = "Ibiza Old Town in Ciudad de Ibiza, Spain",
                    Status = EventStatus.Open,
                    UrlSlug = "tribe-ibiza",
                    Tags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.Name == "Event")
                    }
                },
                new Event
                {
                    Title = "Practical course for Android development in SoftUni",
                    Category = context.Categories.FirstOrDefault(c => c.Name == "Science & Technology"),
                    Date = new DateTime(2015, 5, 28),
                    Duration = new TimeSpan(0, 4, 0, 0),
                    Description = "You are welcome to join us in the free practical course for Android development in Software University",
                    IsActive = true,
                    Location = "Sofia, ul. Tintyava 15-17, 2nd floor",
                    Status = EventStatus.Open,
                    UrlSlug = "practical-course-for-android-development-in-softuni",
                    Tags = new List<Tag>
                    {
                        context.Tags.FirstOrDefault(t => t.Name == "Software"),
                        context.Tags.FirstOrDefault(t => t.Name == "Android"),
                        context.Tags.FirstOrDefault(t => t.Name == "Free")
                    }
                }
            };

            foreach (var e in events)
            {
                context.Events.Add(e);
            }

            context.SaveChanges();
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

                    if (user.UserName == "admin")
                    {
                        userManager.AddToRole(user.Id, "Admin");
                    }
                }
            }

            userStore.Context.SaveChanges();
        }
    }
}