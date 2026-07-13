using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.BuildingBlocks.Events
{
    public abstract class EntityBase : IHasDomainEvents
    {
        private readonly List<INotification> _domainEvents = new();
        public IReadOnlyList<INotification> DomainEvents => _domainEvents.AsReadOnly();

        protected void AddDomainEvent(INotification domainEvent) => _domainEvents.Add(domainEvent);
        public void ClearDomainEvents() => _domainEvents.Clear();
    }
}
