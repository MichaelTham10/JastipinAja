using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Domain
{
    internal enum OrderStatus
    {
        Requested, Accepted, Paid, Purchased,
        InTransit, ReadyForHandover, Completed,
        Cancelled, Refunded
    }
}
