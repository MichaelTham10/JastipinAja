using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.CreatePayment
{
    internal sealed record CreatePaymentRequest(Guid OrderPublicId, decimal amount);
}
