using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.BuildingBlocks.Events
{
    public interface IHasDomainEvents
    {
        IReadOnlyList<INotification> DomainEvents { get; }
        void ClearDomainEvents();
    }
}
