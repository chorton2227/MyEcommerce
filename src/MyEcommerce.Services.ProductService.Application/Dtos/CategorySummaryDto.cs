namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    public class CategorySummaryDto
    {
        public int NumProducts { get; set; }

        public CategoryReadDto Category { get; set; }
    }
}