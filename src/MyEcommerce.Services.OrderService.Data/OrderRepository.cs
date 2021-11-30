namespace MyEcommerce.Services.OrderService.Data
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate;

    public class OrderRepository : IOrderRepository
    {
        private readonly OrderContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public OrderRepository(OrderContext context)
        {
            _context = context;
        }

        public void Create(Order order)
        {
            _context.Orders.Add(order);
        }

        public PaginatedOrders GetAll(OrderOptions options)
        {
            // Validate options
            var isValid = options.Validate();
            if (!isValid) {
                return null;
            }

            // Begin query
            var query = _context
                .Orders
                .Include(o => o.OrderItems)
                .Include(o => o.Status)
                .Include(o => o.BillingAddress)
                .Include(o => o.DeliveryAddress)
                .AsQueryable();
            
            // Filter
            query = query.Where(o => o.UserId == options.UserId);

            // Sort
            query = query.OrderByDescending(o => o.OrderDate);
            
            // Get count
            var totalOrders = query.Count();

            // Paginate
            var skip = options.PageIndex * options.PageLimit;
            var take = options.PageLimit;
            if (options.CheckForMoreRecords)
            {
                take += 1;
            }

            // Return query results
            return new PaginatedOrders
            {
                PageIndex = options.PageIndex,
                PageLimit = options.PageLimit,
                TotalOrders = totalOrders,
                Orders = query
                    .Skip(skip)
                    .Take(take)
                    .ToList()
            };
        }

        public Order GetOne(OrderId orderId, string userId)
        {
            return _context
                .Orders
                .FirstOrDefault(o =>
                    o.Id == orderId && o.UserId == userId
                );
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}