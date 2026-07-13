using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.BuildingBlocks.Events
{
    public static class DomainEventDispatcher
    {
        public static async Task DispatchAndClearEvents(
            this DbContext db, IPublisher publisher, CancellationToken ct)
        {
            var entitiesWithEvents = db.ChangeTracker.Entries<IHasDomainEvents>()
                .Select(e => e.Entity)
                .Where(e => e.DomainEvents.Any())
                .ToList();

            var allEvents = entitiesWithEvents.SelectMany(e => e.DomainEvents).ToList();
            entitiesWithEvents.ForEach(e => e.ClearDomainEvents());

            foreach (var domainEvent in allEvents)
                await publisher.Publish(domainEvent, ct);
        }
    }
}
