using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.ReleasePayment
{
    internal sealed record ReleasePaymentCommand(Guid PublicId);
}
