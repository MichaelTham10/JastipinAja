using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.RefundPayment
{
    internal sealed record RefundPaymentCommand(Guid PublicId);
}
