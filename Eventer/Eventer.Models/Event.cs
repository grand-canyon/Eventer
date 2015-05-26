using System.ComponentModel;

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
            this.IsActive = true;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Event title is required!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event Start date is required!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The event should have duration!")]
        public TimeSpan Duration { get; set; }

        public decimal? Cost { get; set; }

        [Required(ErrorMessage = "Event Location is required!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Event description is required!")]
        public string Description { get; set; }

        [Range(1, int.MaxValue)]
        public int? Limit { get; set; }

        public bool IsActive { get; set; }

        public byte[] Image { get; set; }

        public EventStatus Status { get; set; }

        [Required(ErrorMessage = "Event Category is required!")]
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