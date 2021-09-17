namespace MyEcommerce.Data.EntityFrameworkCore
{
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using MyEcommerce.Core.Domain;

    public static class MediatorExtensions
    {
        public static async Task PublishDomainEventsAsync(this IMediator mediator, DbContext context)
        {
            var entities = context
                .ChangeTracker
                .Entries<IEntity>()
                .Where(entry => entry.Entity.DomainEvents?.Any() == true)
                .Select(entry => entry.Entity);

            foreach (var entity in entities)
            {
                foreach (var domainEvent in entity.DomainEvents)
                {
                    await mediator.Publish(domainEvent).ConfigureAwait(false);
                }

                entity.ClearDomainEvents();
            }
        }
    }
}