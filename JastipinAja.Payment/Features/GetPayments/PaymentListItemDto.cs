using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.GetPayments
{

    internal sealed record PaymentListItemDto(
    Guid PublicId,
    string PaymentNo,
    decimal amount,
    string Status);
}
