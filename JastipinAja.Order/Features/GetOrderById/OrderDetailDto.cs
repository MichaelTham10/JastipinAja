using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.GetOrderById
{
    internal sealed record OrderDetailDto(
        Guid PublicId,
        string OrderNo,
        Guid CustomerId,
        string ItemDescription,
        decimal Price,
        string Status,
        DateTime CreatedAt);
}
