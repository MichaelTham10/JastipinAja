using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.CapturePayment
{
    internal sealed record CapturePaymentCommand(Guid PublicId);

}
