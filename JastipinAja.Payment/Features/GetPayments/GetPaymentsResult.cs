using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.GetPayments
{
    internal sealed record GetPaymentsResult(
    IReadOnlyList<PaymentListItemDto> Items,
    int Page,
    int PageSize,
    int TotalCount);
}
