namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    using System.Collections.Generic;

    public class ProductOptionsDto
    {
        public int PageIndex { get; set; }

        public int PageLimit { get; set; }

        public ProductPageSortDto PageSort { get; set; }

        public bool FilterIsNew { get; set; }

        public bool FilterOnSale { get; set; }

        public decimal? FilterMinPrice { get; set; }

        public decimal? FilterMaxPrice { get; set; }

        public CategoryReadDto FilterCategory { get; set; }

        public IEnumerable<string> FilterTags { get; set; }
    }
}