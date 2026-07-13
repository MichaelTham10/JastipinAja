using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.GetPaymentById
{
    internal sealed record PaymentDetailDto(
    Guid PublicId,
    string PaymentNo,
    decimal Amount,
    string Status,
    DateTime CreatedAt);
}
