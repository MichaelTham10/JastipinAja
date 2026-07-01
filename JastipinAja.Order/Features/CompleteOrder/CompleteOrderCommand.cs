using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.CompleteOrder
{
    internal sealed record CompleteOrderCommand(Guid PublicId);
}
