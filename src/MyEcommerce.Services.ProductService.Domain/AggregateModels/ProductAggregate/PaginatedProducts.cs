namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using System.Collections.Generic;

    public class PaginatedProducts
    {
        public int PageIndex { get; set; }

        public int PageLimit { get; set; }

        public int TotalProducts { get; set; }

        public ICollection<Product> Products { get; set; }

        public ICollection<TagGroupSummary> TagGroupSummaries { get; set; }
    }
}