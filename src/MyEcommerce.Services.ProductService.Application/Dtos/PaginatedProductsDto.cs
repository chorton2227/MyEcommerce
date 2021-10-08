namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    using System.Collections.Generic;

    public class PaginatedProductsDto
    {
        public bool HasMore { get; set; }

        public IEnumerable<ProductReadDto> Products { get; set;}
    }
}