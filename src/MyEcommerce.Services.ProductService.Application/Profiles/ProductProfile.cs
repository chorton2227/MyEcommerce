namespace MyEcommerce.Services.ProductService.Application.Profiles
{
    using System.Linq;
    using AutoMapper;
    using MyEcommerce.Services.ProductService.Application.Dtos;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<PaginatedProducts, PaginatedProductsDto>();

            CreateMap<Product, ProductReadDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
                .ForMember(dest => dest.Categories, opt =>
                    opt.MapFrom(src =>
                        src.ProductCategories.Select(pc => pc.Category)))
                .ForMember(dest => dest.Tags, opt =>
                    opt.MapFrom(src =>
                        src.ProductTags.Select(pc => pc.Tag)));

            CreateMap<ProductOptionsDto, ProductOptions>()
                .ForMember(
                    dest => dest.FilterTagIds, 
                    opt => opt.MapFrom(src =>
                        src.FilterTags.Select(ft =>
                            new TagId(ft)
                        )
                    )
                );

            CreateMap<ProductPageSortDto, ProductPageSort>();
                
            CreateMap<Tag, TagReadDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value));

            CreateMap<TagSummary, TagSummaryDto>();

            CreateMap<TagGroupSummary, TagGroupSummaryDto>();
        }
    }
}