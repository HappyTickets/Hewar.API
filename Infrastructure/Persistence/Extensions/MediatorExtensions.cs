using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Extensions
{
    public static class MediatorExtensions
    {
        public static async Task PublishDomainEvents(this IPublisher mediator, DbContext context)
        {
            var entities = context.ChangeTracker
                .Entries<BaseEntity>()
                .Where(e => e.Entity.DomainEvents.Any())
                .Select(e => e.Entity);

            var domainEvents = entities
                .SelectMany(e => e.DomainEvents)
                .ToList();

            foreach (var entity in entities)
            {
                entity.ClearDomainEvents();
            }

            await Task.WhenAll(domainEvents.Select(domainEvent => mediator.Publish(domainEvent)));
        }
    }
}
