namespace MyEcommerce.Services.ProductService.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyEcommerce.Core.Domain;
    using MyEcommerce.Services.ProductService.Domain.AggregateModels.CatalogAggregate;

    public class CatalogRepository : ICatalogRepository
    {
        private readonly ProductContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CatalogRepository(ProductContext context)
        {
            _context = context;
        }

        public void Create(Catalog catalog)
        {
            _context.Catalogs.Add(catalog);
        }

        public IEnumerable<Catalog> GetAll()
        {
            return _context.Catalogs.ToList();
        }

        public Catalog GetById(CatalogId catalogId)
        {
            return _context.Catalogs.FirstOrDefault(c => c.Id == catalogId);
        }

        public IEnumerable<Category> GetCategoriesByCatalogId(CatalogId catalogId)
        {
            return _context.Categories
                .Where(c => c.Catalog.Id == catalogId)
                .ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }
    }
}