namespace MyEcommerce.Services.OrderService.Application.CommandHandlers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using MyEcommerce.Core.Application.CommandHandlers;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.OrderService.Application.Commands;
    using MyEcommerce.Services.OrderService.Application.Dtos;
    using MyEcommerce.Services.OrderService.Application.Services;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderCreateCommandHandler : ICommandHandler<OrderCreateCommand, OrderDto>
    {
        private readonly IOrderRepository _orderRepository;
        
        private readonly IMapper _mapper;

        private readonly IEmailService _emailService;

        public OrderCreateCommandHandler(IOrderRepository orderRepository, IMapper mapper, IEmailService emailService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<OrderDto> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(
                request.UserId,
                _mapper.Map<Address>(request.OrderCreateDto.DeliveryAddress),
                request.OrderCreateDto.ChargeId,
                request.OrderCreateDto.Email
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
            var result = await _orderRepository.SaveChangesAsync();
            await _emailService.SendOrderReceipt(order);
            return _mapper.Map<OrderDto>(order);
        }
    }
}