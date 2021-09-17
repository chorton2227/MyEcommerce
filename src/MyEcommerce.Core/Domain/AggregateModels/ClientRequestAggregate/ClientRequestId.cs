namespace MyEcommerce.Core.Domain.AggregateModels.ClientRequestAggregate
{
    using System;

    public class ClientRequestId : Identifier
    {
        public ClientRequestId()
        {
        }

        public ClientRequestId(Guid value) : base(value)
        {
        }
    }
}