namespace Eventer.Web.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Eventer.Models;
    using Eventer.Web.Infrastructure.Mappings;

    public class EventViewModel : IMapFrom<Event>
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Event Title is required!")]
        [StringLength(250, MinimumLength = 3)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Event Date is required!")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Event Time is required!")]
        public TimeSpan Time { get; set; }

        [Required(ErrorMessage = "The event should have duration!")]
        public TimeSpan Duration { get; set; }

        public decimal? Cost { get; set; }

        [Required(ErrorMessage = "The event must have a location!")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Event Description is required!")]
        [StringLength(2500, MinimumLength = 30)]
        public string Description { get; set; }

        [Url(ErrorMessage = "The event link must be a valid URL")]
        public string Link { get; set; }

        [Range(1, double.MaxValue)]
        public int? Limit { get; set; }

        public string Image { get; set; }

        public EventStatus Status { get; set; }

        public string Slug { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Event category is required")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public string InputTags { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public virtual ICollection<CommentViewModel> Comments { get; set; }

        public ICollection<User> Participants { get; set; }
    }
}