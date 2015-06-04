namespace Eventer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [MinLength(2)]
        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public string AuthorId { get; set; }
        
        public User Author { get; set; }
    }
}
