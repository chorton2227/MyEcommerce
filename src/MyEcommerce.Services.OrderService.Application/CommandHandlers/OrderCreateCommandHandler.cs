namespace MyEcommerce.Services.OrderService.Application.CommandHandlers
{
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MyEcommerce.Core.Application.CommandHandlers;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.OrderService.Application.Commands;
    using MyEcommerce.Services.OrderService.Application.Dtos;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderCreateCommandHandler : ICommandHandler<OrderCreateCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        
        private readonly IMapper _mapper;

        public OrderCreateCommandHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(
                request.UserId,
                _mapper.Map<Address>(request.OrderCreateDto.BillingAddress),
                _mapper.Map<Address>(request.OrderCreateDto.DeliveryAddress),
                request.OrderCreateDto.ChargeId
            );

            foreach (var orderItemDto in request.OrderCreateDto.OrderItems)
            {
                order.AddOrderItem(new OrderItem(
                    new ProductId(orderItemDto.ProductId),
                    orderItemDto.Name,
                    orderItemDto.Price,
                    orderItemDto.Quantity,
                    orderItemDto.ImageUrl
                ));
            }
            
            _orderRepository.Create(order);
            await _orderRepository.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }
    }
}