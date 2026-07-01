using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.MarkAsPurchased
{
    internal sealed record MarkAsPurchasedCommand(Guid PublicId);
}
