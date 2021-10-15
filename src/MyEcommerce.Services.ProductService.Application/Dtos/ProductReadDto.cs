namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    using System.Collections.Generic;
    
    public class ProductReadDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal? SalePrice { get; set; }

        public string ImageFileName { get; set; }

        public string ImageUri { get; set; }

        public uint AvailableStock { get; set; }

        public uint RestockTreshold { get; set; }

        public uint MaxStockThreshold { get; set; }

        public bool OnReorder { get; set; }

        public bool IsNew { get; set; }

        public bool OnSale { get; set; }

        public IEnumerable<CategoryReadDto> Categories { get; set; }

        public IEnumerable<TagReadDto> Tags { get; set; }
    }
}