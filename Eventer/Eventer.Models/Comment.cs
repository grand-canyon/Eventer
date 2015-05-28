
namespace Eventer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Text { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public int EventId { get; set; }

        [Required]
        public Event Event { get; set; }

        public string AuthorId { get; set; }
        
        [Required]
        public User Author { get; set; }
    }
}
