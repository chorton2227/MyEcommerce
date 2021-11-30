namespace MyEcommerce.Services.OrderService.Application
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using MediatR;
    using MyEcommerce.Core.Application;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.OrderService.Application.Commands;
    using MyEcommerce.Services.OrderService.Application.Dtos;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderApplication : BaseApplication, IOrderApplication
    {
        private readonly IMapper _mapper;

        private readonly IOrderRepository _orderRepository;

        public OrderApplication(
            IMediator mediator,
            IMapper mapper,
            IOrderRepository orderRepository
        ) : base(mediator) {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderCreateCommand command)
        {
            return await Handle(command).ConfigureAwait(false);
        }

        public OrderDto GetOrder(string orderId, string userId)
        {
            var order = _orderRepository.GetOne(new OrderId(orderId), userId);
            return _mapper.Map<OrderDto>(order);
        }

        public PaginatedOrdersDto GetOrders(OrderOptionsDto optionsDto)
        {
            var options = _mapper.Map<OrderOptions>(optionsDto);

            // Get 1 more record to check for has more
            options.CheckForMoreRecords = true;

            // Get orders, check if has more, and remove the extra order
            var paginatedOrders = _orderRepository.GetAll(options);
            var hasMore = paginatedOrders.Orders.Count() > options.PageLimit;
            if (hasMore) {
                paginatedOrders.Orders = paginatedOrders.Orders
                    .Take(options.PageLimit)
                    .ToList();
            }

            // Return dto
            var paginatedOrdersDto = _mapper.Map<PaginatedOrdersDto>(paginatedOrders);
            paginatedOrdersDto.HasMore = hasMore;
            return paginatedOrdersDto;
        }
    }
}