namespace MyEcommerce.Services.ProductService.Domain.AggregateModels.ProductAggregate
{
    using System.Collections.Generic;
    
    public class ProductOptions
    {
        private const int DEFAULT_PAGE_INDEX = 0;
        
        private const int DEFAULT_PAGE_LIMIT = 12;
        
        private const int MAX_PAGE_LIMIT = 51;

        private const decimal DEFAULT_FILTER_MIN_PRICE = 0.01M;

        public int PageIndex { get; set; }

        public int PageLimit { get; set; }

        public bool CheckForMoreRecords { get; set; }

        public ProductPageSort PageSort { get; set; }

        public bool FilterIsNew { get; set; }

        public bool FilterOnSale { get; set; }

        public decimal? FilterMinPrice { get; set; }

        public decimal? FilterMaxPrice { get; set; }

        public string FilterCategoryName { get; set; }

        public IEnumerable<TagId> FilterTagIds { get; set; }

        public ProductOptions()
        {
            // Default options
            PageIndex = DEFAULT_PAGE_INDEX;
            PageLimit = DEFAULT_PAGE_LIMIT;
            CheckForMoreRecords = true;
            PageSort = ProductPageSort.DEFAULT;
            FilterIsNew = false;
            FilterOnSale = false;
            FilterMinPrice = DEFAULT_FILTER_MIN_PRICE;
            FilterMaxPrice = null;
            FilterCategoryName = null;
            FilterTagIds = new List<TagId>();
        }

        public void Validate()
        {
            // Require page index to be 0 or positive
            if (PageIndex < 0)
            {
                PageIndex = DEFAULT_PAGE_INDEX;
            }

            // Require 1 or MAX products on a page; otherwise DEFAULT
            if (PageLimit < 1 || PageLimit > MAX_PAGE_LIMIT)
            {
                PageLimit = DEFAULT_PAGE_LIMIT;
            }

            // Require MIN price to be less than MAX price;
            // otherwise set MIN to DEFAULT or clear MAX
            if (FilterMinPrice.HasValue && FilterMaxPrice.HasValue && FilterMinPrice >= FilterMaxPrice)
            {
                if (FilterMinPrice > DEFAULT_FILTER_MIN_PRICE)
                {
                    FilterMinPrice = DEFAULT_FILTER_MIN_PRICE;
                } else {
                    FilterMaxPrice = null;
                }
            }

            // Require MIN price to be greater than 0
            if (FilterMinPrice.HasValue && FilterMinPrice < DEFAULT_FILTER_MIN_PRICE)
            {
                FilterMinPrice = DEFAULT_FILTER_MIN_PRICE;
            }

            // Require MAX price to be greater than 0
            if (FilterMaxPrice.HasValue && FilterMaxPrice < DEFAULT_FILTER_MIN_PRICE)
            {
                FilterMaxPrice = DEFAULT_FILTER_MIN_PRICE;
            }
        }
    }
}