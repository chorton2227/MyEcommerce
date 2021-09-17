namespace MyEcommerce.Core.Domain.AggregateModels.ClientRequestAggregate
{
    using System;

    public class ClientRequest : Entity<ClientRequestId>, IAggregateRoot
    {
        public DateTimeOffset RequestDate { get; protected set; }

        public ClientRequest(ClientRequestId clientRequestId)
        {
            Id = clientRequestId;
            RequestDate = DateTimeOffset.UtcNow;
        }
    }
}