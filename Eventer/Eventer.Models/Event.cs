namespace Eventer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Event
    {
        private ICollection<User> participants;

        public Event()
        {
            this.participants = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public DateTime ExpireDate { get; set; }

        [Required]
        public string EventLocation { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, double.MaxValue)]
        public int Limit { get; set; }

        public bool IsActive { get; set; }

        public ICollection<User> Participants
        {
            get { return this.participants; }
            set { this.participants = value; }
        }
    }
}