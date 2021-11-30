namespace MyEcommerce.Core.Domain.Common
{
    using System;
    
    public class OrderId : Identifier
    {
        public OrderId()
        {
        }

        public OrderId(Guid value) : base(value)
        {
        }

        public OrderId(string value) : base(value)
        {
        }
    }
}