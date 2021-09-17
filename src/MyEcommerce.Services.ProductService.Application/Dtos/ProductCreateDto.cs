namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    using System.ComponentModel.DataAnnotations;

    public class ProductCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImageFileName { get; set; }

        public string ImageUri { get; set; }

        public uint AvailableStock { get; set; }

        public uint RestockTreshold { get; set; }

        public uint MaxStockThreshold { get; set; }

        public bool OnReorder { get; set; }
    }
}