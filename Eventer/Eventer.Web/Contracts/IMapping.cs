namespace Eventer.Web.Contracts
{
    using AutoMapper;

    public interface IMapping
    {
        void CreateMappings(IConfiguration configuration);
    }
}