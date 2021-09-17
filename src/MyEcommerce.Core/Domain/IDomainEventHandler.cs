namespace MyEcommerce.Core.Domain
{
    using MediatR;
    
    public interface IDomainEventHandler<TDomainEvent> : INotificationHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
    }
}