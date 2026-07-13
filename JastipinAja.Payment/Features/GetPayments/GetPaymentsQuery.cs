using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.GetPayments
{
    internal sealed record GetPaymentsQuery(int Page = 1, int PageSize = 20);
}
