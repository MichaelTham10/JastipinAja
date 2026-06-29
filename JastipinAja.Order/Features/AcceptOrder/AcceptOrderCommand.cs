using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.AcceptOrder
{
    internal sealed record AcceptOrderCommand(Guid PublicId);
}
