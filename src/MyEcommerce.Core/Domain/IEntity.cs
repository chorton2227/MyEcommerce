namespace MyEcommerce.Core.Domain
{
    using System.Collections.Generic;
    
    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent domainEvent);

        void RemoveDomainEvent(IDomainEvent domainEvent);

        void ClearDomainEvents();
    }
}