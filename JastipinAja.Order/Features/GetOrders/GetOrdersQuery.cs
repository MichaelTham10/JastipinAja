using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.GetOrders
{
    internal sealed record GetOrdersQuery(int Page = 1, int PageSize = 20);

}
