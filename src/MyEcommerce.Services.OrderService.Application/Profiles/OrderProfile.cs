namespace MyEcommerce.Services.OrderService.Application.Profiles
{
    using AutoMapper;
	using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.OrderService.Application.Dtos;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<string, ProductId>()
                .ConstructUsing(value => new ProductId(value));

            CreateMap<Order, OrderDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id.Value)
                );

            CreateMap<OrderCreateDto, Order>();

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id.Value)
                )
                .ForMember(
                    dest => dest.ProductId,
                    opt => opt.MapFrom(src => src.ProductId.Value)
                );

            CreateMap<OrderItemDto, OrderItem>();

            CreateMap<OrderStatus, OrderStatusDto>();

            CreateMap<Address, AddressDto>();

            CreateMap<AddressDto, Address>();

            CreateMap<OrderOptionsDto, OrderOptions>();

            CreateMap<PaginatedOrders, PaginatedOrdersDto>();
        }
    }
}