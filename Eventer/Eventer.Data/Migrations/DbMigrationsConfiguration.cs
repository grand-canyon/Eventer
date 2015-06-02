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
            SeedCategories(context);
            SeedTags(context);
            SeedEvents(context);
            SeedRoles(context);
            SeedUsers(context);
        }

        private static void SeedCategories(IEventerDbContext context)
        {
            var categories = new List<Category>
            {
                new Category {Name = "Concerts & Music", Slug = "concerts-music"},
                new Category {Name = "Business & Professional", Slug = "business-professional"},
                new Category {Name = "Science & Technology", Slug = "science-technology"},
                new Category {Name = "Performing & Visual Arts", Slug = "performing-visual-arts"},
                new Category {Name = "Travel & Outdoor", Slug = "travel-outdoor"},
                new Category {Name = "Sport & Fitness", Slug = "sport-fitness"},
                new Category {Name = "Other", Slug = "other"},
            };

            foreach (var category in categories)
            {
                context.Categories.AddOrUpdate(c => c.Name, category);
            }

            context.SaveChanges();
        }

        private static void SeedTags(IEventerDbContext context)
        {
            var tags = new List<Tag>
            {
                new Tag {Name = "Free", Slug = "free"},
                new Tag {Name = "Experience", Slug = "experience"},
                new Tag {Name = "Jobs", Slug = "jobs"},
                new Tag {Name = "Programming", Slug = "programming"},
                new Tag {Name = "Android", Slug = "android"},
                new Tag {Name = "Development", Slug = "development"},
                new Tag {Name = "Software", Slug = "software"},
                new Tag {Name = "Event", Slug = "event"}
            };

            foreach (var tag in tags)
            {
                context.Tags.AddOrUpdate(t => t.Name, tag);
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
                    Location = "Ibiza Old Town in Ciudad de Ibiza, Spain",
                    Status = EventStatus.Open,
                    Slug = "tribe-ibiza",
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
                    Location = "Sofia, ul. Tintyava 15-17, 2nd floor",
                    Status = EventStatus.Open,
                    Slug = "practical-course-for-android-development-in-softuni",
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
                if (!context.Events.Any(x => x.Title == e.Title))
                {
                    context.Events.AddOrUpdate(x => x.Title, e);
                }
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
                    UserName = "admin",
                    Email = "admin@eventer.com",
                    Events = new List<Event>
                    {
                        context.Events.FirstOrDefault(e => e.Title.Contains("Tribe Ibiza")),
                        context.Events.FirstOrDefault(e => e.Title.Contains("Practical course for Android"))
                    }
                },
                new User
                {
                    FirstName = "Nina",
                    LastName = "Markova",
                    UserName = "Markova",
                    Email = "nina.n.markova@gmail.com",
                    Avatar = "https://softuni.bg/Users/Profile/ShowAvatar/f1b52eba-225e-45dd-b803-30be4f789747"
                },
                new User
                {
                    FirstName = "Christopher",
                    LastName = "Savov",
                    UserName = "f1mp3r",
                    Email = "craizup@gmail.com",
                    Avatar = "https://softuni.bg/Users/Profile/ShowAvatar/bb4df91d-ef74-4b34-9a87-bc62dc881e84"
                },
                new User
                {
                    FirstName = "Karim",
                    LastName = "Hristov",
                    UserName = "Flyer",
                    Email = "unbelt@outlook.com",
                    Avatar = "https://softuni.bg/Users/Profile/ShowAvatar/b7b69109-2893-4d88-9d53-39feb38bb8b5"
                }
            };

            foreach (var user in users)
            {
                if (userManager.FindByName(user.UserName) == null)
                {
                    userManager.Create(user, "pass123");
                    userManager.SetLockoutEnabled(user.Id, false);
                    userManager.AddToRole(user.Id, "User");

                    if (user.UserName == "admin" || user.UserName == "Flyer" || user.UserName == "Markova" || user.UserName == "f1mp3r")
                    {
                        userManager.AddToRole(user.Id, "Admin");
                    }
                }
            }

            userStore.Context.SaveChanges();
        }
    }
}