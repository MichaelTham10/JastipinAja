using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.MarkAsPaid
{
    internal sealed record MarkAsPaidCommand(Guid PublicId);
}
