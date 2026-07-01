using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.RefundOrder
{
    internal sealed record RefundOrderCommand(Guid PublicId);
}
