namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    public class TagSummaryDto
    {
        public int NumProducts { get; set; }
        
        public TagReadDto Tag { get; set; }
    }
}