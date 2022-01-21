namespace MyEcommerce.Services.OrderService.Data
{
    using System;
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

        public Order Create(Order order)
        {
            return _context.Orders.Add(order).Entity;
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
                .AsQueryable();
            
            // Filter
            query = query.Where(o => o.UserId == options.UserId);
            
            // Get count
            var totalOrders = query.Count();

            // Sort
            var orderedQuery = query.OrderByDescending(o => o.OrderDate);

            // Paginate
            var skip = options.PageIndex * options.PageLimit;
            var take = options.PageLimit;
            if (options.CheckForMoreRecords)
            {
                take += 1;
            }

            // Return query results
            var orders = orderedQuery
                .Skip(skip)
                .Take(take)
                .ToList();
            Console.WriteLine("Order Count: " + orders.Count().ToString());
            Console.WriteLine("Skip: " + skip.ToString());
            Console.WriteLine("Take: " + take.ToString());
            return new PaginatedOrders
            {
                PageIndex = options.PageIndex,
                PageLimit = options.PageLimit,
                TotalOrders = totalOrders,
                Orders = orders
            };
        }

        public Order GetOne(OrderId orderId, string userId)
        {
            return _context
                .Orders
                .Include(o => o.OrderItems)
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