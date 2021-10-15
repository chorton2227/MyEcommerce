namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    using System.Collections.Generic;

    public class PaginatedProductsDto
    {
        public bool HasMore { get; set; }
        
        public int PageIndex { get; set; }

        public int PageLimit { get; set; }

        public int TotalProducts { get; set; }

        public IEnumerable<ProductReadDto> Products { get; set; }

        public IEnumerable<TagGroupSummaryDto> TagGroupSummaries { get; set; }
    }
}