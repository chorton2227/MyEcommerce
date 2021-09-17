namespace MyEcommerce.Services.ProductService.Data
{
    using System.Threading.Tasks;
    using MyEcommerce.Core.Domain;
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
    }
}