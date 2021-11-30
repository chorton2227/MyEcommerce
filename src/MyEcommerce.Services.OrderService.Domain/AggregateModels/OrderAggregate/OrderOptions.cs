namespace MyEcommerce.Services.OrderService.Domain.AggregateModels.OrderAggregate
{
    public class OrderOptions
    {
        private const int DEFAULT_PAGE_INDEX = 0;

        private const int DEFAULT_PAGE_LIMIT = 10;

        private const int MAX_PAGE_LIMIT = 51;

        public int PageIndex { get; set; }

        public int PageLimit { get; set; }

        public string UserId { get; set; }

        public bool CheckForMoreRecords { get; set; }

        public OrderOptions()
        {
            // Default Options
            PageIndex = DEFAULT_PAGE_INDEX;
            PageLimit = DEFAULT_PAGE_LIMIT;
        }

        public bool Validate()
        {
            // Require a user id
            if (string.IsNullOrWhiteSpace(UserId))
            {
                return false;
            }

            // Check page index
            if (PageIndex < 0)
            {
                PageIndex = DEFAULT_PAGE_INDEX;
            }

            // Check page limit
            if (PageLimit < 1 || PageLimit > MAX_PAGE_LIMIT)
            {
                PageLimit = DEFAULT_PAGE_LIMIT;
            }

            return true;
        }
    }
}