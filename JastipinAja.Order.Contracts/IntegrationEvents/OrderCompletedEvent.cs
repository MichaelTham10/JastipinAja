using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Contracts.IntegrationEvents
{
    public sealed record OrderCompletedEvent(Guid OrderPublicId, decimal Amount) : INotification;
}
