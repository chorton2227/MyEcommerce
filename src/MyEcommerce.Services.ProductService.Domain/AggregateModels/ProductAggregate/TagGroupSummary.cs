namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using System.Collections.Generic;
    
    public class TagGroupSummary
    {
        public string Group { get; set; }
        
        public ICollection<TagSummary> TagSummaries { get; set; }
    }
}