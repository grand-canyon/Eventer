namespace Eventer.Web.InputModels
{
    using System;

    using Models;
    using Infrastructure.Mappings;

    public class CommentInputModel : IMapTo<Comment>
    {
        public string Text { get; set; }

        public string AuthorId { get; set; }

        public int EventId { get; set; }

        public DateTime DateCreated { get; set; }
    }
}