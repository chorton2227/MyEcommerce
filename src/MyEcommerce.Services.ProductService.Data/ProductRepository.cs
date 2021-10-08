namespace MyEcommerce.Services.ProductService.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Core.Domain.Common;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate;

    public class ProductRepository : IProductRepository
    {
        private readonly ProductContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProductRepository(ProductContext context)
        {
            _context = context;
        }

        public void Create(Product product)
        {
            _context.Products.Add(product);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public IEnumerable<Product> GetAll(int page, int size)
        {
            return _context
                .Products
                .Skip(page * size)
                .Take(size)
                .ToList();
        }

        public Product GetById(ProductId productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }
    }
}