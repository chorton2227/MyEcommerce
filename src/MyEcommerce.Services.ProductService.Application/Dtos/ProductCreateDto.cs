namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        public decimal? SalePrice { get; set; }

        public string ImageFileName { get; set; }

        public string ImageUri { get; set; }

        public uint AvailableStock { get; set; }

        public uint RestockTreshold { get; set; }

        public uint MaxStockThreshold { get; set; }

        public bool OnReorder { get; set; }

        public bool IsNew { get; set; }

        public IEnumerable<CategoryReadDto> Categories { get; set; }

        public IEnumerable<TagReadDto> Tags { get; set; }
    }
}