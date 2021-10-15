namespace MyEcommerce.Services.ProductService.Application.Dtos
{
    using System.Collections.Generic;
    
    public class TagGroupSummaryDto
    {
        public string Group { get; set; }
        
        public List<TagSummaryDto> TagSummaries { get; set; }

        public TagGroupSummaryDto()
        {
            TagSummaries = new List<TagSummaryDto>();
        }
    }
}