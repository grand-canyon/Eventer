﻿namespace Eventer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Event> events;

        public User()
        {
            this.events = new HashSet<Event>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.LastName);
            }
        }

        public string Avatar { get; set; }

        public virtual ICollection<Event> Events
        {
            get { return this.events; }
            set { this.events = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }
    }
}