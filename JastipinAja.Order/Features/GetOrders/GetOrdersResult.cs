using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.GetOrders
{
    internal sealed record GetOrdersResult(
        IReadOnlyList<OrderListItemDto> Items,
        int Page,
        int PageSize,
        int TotalCount);
}
