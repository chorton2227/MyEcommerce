namespace MyEcommerce.Services.ProductService.Application.Profiles
{
    using AutoMapper;
    using MyEcommerce.Services.ProductService.Application.Dtos;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductReadDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));
        }
    }
}