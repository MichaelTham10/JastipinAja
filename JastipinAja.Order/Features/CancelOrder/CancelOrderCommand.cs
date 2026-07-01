using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.CancelOrder
{
    internal sealed record CancelOrderCommand(Guid PublicId);
}
