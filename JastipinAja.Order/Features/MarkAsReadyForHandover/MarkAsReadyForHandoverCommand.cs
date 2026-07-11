using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.MarkAsReadyForHandover
{
    internal sealed record MarkAsReadyForHandoverCommand(Guid PublicId);
}
