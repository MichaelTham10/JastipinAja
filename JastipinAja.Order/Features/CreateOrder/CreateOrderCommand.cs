using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.CreateOrder
{
    internal sealed record CreateOrderCommand(Guid CustomerId, string ItemDescription, decimal Price);
}
