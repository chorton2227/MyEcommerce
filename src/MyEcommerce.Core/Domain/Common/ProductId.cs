namespace MyEcommerce.Core.Domain.Common
{
    using System;
    
    public class ProductId : Identifier
    {
        public ProductId()
        {
        }

        public ProductId(Guid value) : base(value)
        {
        }
    }
}