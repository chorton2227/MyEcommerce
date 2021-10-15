namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    public class TagSummary
    {
        public int NumProducts { get; set; }
        
        public Tag Tag { get; set; }
    }
}