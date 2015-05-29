namespace Eventer.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Models;

    public class EventViewModel
    {
        private ICollection<Tag> tags;
        private ICollection<Comment> comments;
        private ICollection<User> participants;

        [Required(ErrorMessage = "Event Title is required!")]
        [StringLength(250, MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event Date is required!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "The event must have a location!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Event Description is required!")]
        public string Description { get; set; }

        [Range(1, double.MaxValue)]
        public int? Limit { get; set; }

        public byte[] Image { get; set; }

        public bool IsActive { get; set; }

        public virtual EventStatus Status { get; set; }

        [Required(ErrorMessage = "The event must have a category!")]
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

        public ICollection<User> Participants
        {
            get { return this.participants; }
            set { this.participants = value; }
        }
    }
}