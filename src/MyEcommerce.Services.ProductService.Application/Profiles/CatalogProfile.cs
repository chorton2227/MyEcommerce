namespace MyEcommerce.Services.ProductService.Application.Profiles
{
    using AutoMapper;
    using MyEcommerce.Services.ProductService.Application.Dtos;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;

    public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<Category, CategoryReadDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}