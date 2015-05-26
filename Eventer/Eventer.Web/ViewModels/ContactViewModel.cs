namespace Eventer.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ContactViewModel
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Subject { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 20)]
        public string Message { get; set; }
    }
}