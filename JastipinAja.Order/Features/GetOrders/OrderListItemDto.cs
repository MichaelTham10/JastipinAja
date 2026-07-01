using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.GetOrders
{
    internal sealed record OrderListItemDto(
        Guid PublicId,
        string OrderNo,
        string ItemDescription,
        decimal Price,
        string Status);
}
