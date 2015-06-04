namespace Eventer.Web.InputModels
{
    using Models;
    using Infrastructure.Mappings;
    using System;

    public class CommentInputModel : IMapTo<Comment>
    {
        public string Text { get; set; }

        public string AuthorId { get; set; }

        public int EventId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}