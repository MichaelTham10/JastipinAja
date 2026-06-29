using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.CreateOrder
{
    internal sealed record CreateOrderResult(Guid PublicId, string OrderNo);
}
