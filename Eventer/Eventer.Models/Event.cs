using System.Security.Policy;

namespace Eventer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Event
    {
        private ICollection<Tag> tags;
        private ICollection<Comment> comments;
        private ICollection<User> participants;

        private string slug;

        public Event()
        {
            this.tags = new HashSet<Tag>();
            this.comments = new HashSet<Comment>();
            this.participants = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250, MinimumLength = 5)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event Start date is required!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The event should have duration!")]
        public TimeSpan Duration { get; set; }

        public decimal? Cost { get; set; }

        [Required(ErrorMessage = "Event Location is required!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Event description is required!")]
        [StringLength(2500, MinimumLength = 30)]
        public string Description { get; set; }

        [Url]
        public string Link { get; set; }

        [Range(1, int.MaxValue)]
        public int? Limit { get; set; }

        [Column(TypeName = "ntext")]
        public string Image { get; set; }

        public EventStatus Status { get; set; }

        public string Slug
        {
            get { return this.slug; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.slug = this.Title.Replace(" ", "-").ToLower();
                }
            }
        }

        [Required(ErrorMessage = "Event Category is required!")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<User> Participants
        {
            get { return this.participants; }
            set { this.participants = value; }
        }
    }
}