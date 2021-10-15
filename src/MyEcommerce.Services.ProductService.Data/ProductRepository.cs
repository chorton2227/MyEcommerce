namespace MyEcommerce.Services.ProductService.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
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

        public PaginatedProducts GetAll(ProductOptions options)
        {
            // Call to validate options and set defaults
            options.Validate();

            /* Begin query */

            var query = _context
                .Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                //.ThenInclude(t => t.Id)
                .AsQueryable();

            /* Filter query */

            if (options.FilterIsNew)
            {
                query = query.Where(p => p.IsNew);
            }

            if (options.FilterOnSale)
            {
                query = query.Where(p => p.SalePrice.HasValue);
            }

            if (options.FilterMinPrice.HasValue)
            {
                query = query.Where(p =>
                    p.SalePrice.HasValue
                        ? p.SalePrice >= options.FilterMinPrice
                        : p.Price >= options.FilterMinPrice
                );
            }

            if (options.FilterMaxPrice.HasValue)
            {
                query = query.Where(p =>
                    p.SalePrice.HasValue
                        ? p.SalePrice <= options.FilterMaxPrice
                        : p.Price <= options.FilterMaxPrice
                );
            }

            if (!string.IsNullOrWhiteSpace(options.FilterCategoryName))
            {
                query = query.Where(p =>
                    p.ProductCategories.Any(pc =>
                        pc.Category.Name == options.FilterCategoryName
                    )
                );
            }

            if (options.FilterTagIds != null && options.FilterTagIds.Any())
            {
                query = query.Where(p =>
                    p.ProductTags.Any(pt =>
                        options.FilterTagIds.Contains(pt.Tag.Id)
                    )
                );
            }

            /* Sort query */
            
            IOrderedQueryable<Product> orderedQuery = null;
            switch (options.PageSort) {
                case ProductPageSort.NEW:
                    orderedQuery = query
                        .OrderByDescending(p => p.IsNew)
                        .ThenBy(p => p.Name)
                        .ThenByDescending(p =>
                            p.SalePrice.HasValue
                                ? p.SalePrice
                                : p.Price
                        )
                        .ThenByDescending(p => p.Id.Value);
                    break;
                case ProductPageSort.PRICE_LOW_HIGH:
                    orderedQuery = query
                        .OrderBy(p =>
                            p.SalePrice.HasValue
                                ? p.SalePrice
                                : p.Price
                        )
                        .ThenBy(p => p.Name)
                        .ThenByDescending(p => p.Id.Value);
                    break;
                case ProductPageSort.PRICE_HIGH_LOW:
                    orderedQuery = query
                        .OrderByDescending(p =>
                            p.SalePrice.HasValue
                                ? p.SalePrice
                                : p.Price
                        )
                        .ThenBy(p => p.Name)
                        .ThenByDescending(p => p.Id.Value);
                    break;
                case ProductPageSort.DEFAULT:
                default:
                    orderedQuery = query
                        .OrderBy(p => p.Name)
                        .ThenByDescending(p =>
                            p.SalePrice.HasValue
                                ? p.SalePrice
                                : p.Price
                        )
                        .ThenByDescending(p => p.Id.Value);
                    break;
            }

            /* Count query */
            
            var totalProducts = query.Count();

            /* Get tag group summaries that match query */

            var tagSummaries = query
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .AsEnumerable()
                .SelectMany(p => p.ProductTags)
                .OrderBy(t => t.Tag.Group)
                .ThenBy(t => t.Tag.Name)
                .GroupBy(t => t.Tag)
                .Select(g => new TagSummary
                {
                    NumProducts = g.Count(),
                    Tag = g.Key
                })
                .ToList();

            var tagGroupSummaries = new List<TagGroupSummary>();
            foreach (var tagSummary in tagSummaries)
            {
                var tagGroupSummary = tagGroupSummaries.FirstOrDefault(tg => tg.Group == tagSummary.Tag.Group);
                if (tagGroupSummary == null)
                {
                    tagGroupSummary = new TagGroupSummary
                    {
                        Group = tagSummary.Tag.Group,
                        TagSummaries = new List<TagSummary>(),
                    };
                    tagGroupSummaries.Add(tagGroupSummary);
                }

                tagGroupSummary.TagSummaries.Add(tagSummary);
            }

            /* Paginate query */
            
            var skip = options.PageIndex * options.PageLimit;
            var take = options.PageLimit;
            if (options.CheckForMoreRecords)
            {
                take += 1;
            }

            var products = orderedQuery
                .Skip(skip)
                .Take(take)
                .ToList();

            return new PaginatedProducts
            {
                PageIndex = options.PageIndex,
                PageLimit = options.PageLimit,
                TotalProducts = totalProducts,
                Products = products,
                TagGroupSummaries = tagGroupSummaries
            };
        }

        public Product GetById(ProductId productId)
        {
            return _context.Products.FirstOrDefault(p => p.Id == productId);
        }
    }
}