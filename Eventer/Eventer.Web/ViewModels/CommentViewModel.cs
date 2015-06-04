namespace Eventer.Web.ViewModels
{
    using System;
    using Models;
    using Infrastructure.Mappings;

    public class CommentViewModel : IMapFrom<Comment>, IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string Username { get; set; }

        public DateTime DateCreated { get; set; }

        public int EventId { get; set; }

        public void CreateMappings(AutoMapper.IConfiguration configuration)
        {
            configuration.CreateMap<Comment, CommentViewModel>()
                .ForMember(m => m.Username, cfg => cfg.MapFrom(e => e.Author.UserName))
                .ForMember(m => m.EventId, cfg => cfg.MapFrom(e => e.EventId));
        }
    }
}