using API.Entities;
using API.Entities.ViewModels;
using AutoMapper;

namespace API.Mappers
{
    public class EntityToViewModelMapping : Profile
    {
        public EntityToViewModelMapping()
        {
            CreateMap<News, NewsViewModel>();
        }
    }
}
