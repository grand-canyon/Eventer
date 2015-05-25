namespace Eventer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Event
    {
        private ICollection<Tag> tags;
        private ICollection<User> participants;

        public Event()
        {
            this.tags = new HashSet<Tag>();
            this.participants = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal? Cost { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [Range(1, double.MaxValue)]
        public int? Limit { get; set; }

        public bool IsActive { get; set; }

        public byte[] Image { get; set; }

        public EventStatus Status { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<User> Participants
        {
            get { return this.participants; }
            set { this.participants = value; }
        }
    }
}