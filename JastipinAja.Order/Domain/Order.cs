using JastipinAja.BuildingBlocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Domain
{
    internal sealed class Order
    {
        public long Id { get; private set; }
        public Guid PublicId { get; private set; }
        public string OrderNo { get; private set; }
        public Guid CustomerId { get; private set; }
        public string ItemDescription { get; private set; } = string.Empty;
        public decimal Price { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Order() { } // dipakai EF Core saat memuat dari DB

        public Order(Guid customerId, string orderNo, string itemDescription, decimal price)
        {
            if (customerId == Guid.Empty)
                throw new DomainException("CustomerId wajib diisi.");
            if (string.IsNullOrWhiteSpace(itemDescription))
                throw new DomainException("Deskripsi barang wajib diisi.");
            if (price <= 0)
                throw new DomainException("Harga harus lebih dari nol.");
            PublicId = Guid.NewGuid();
            OrderNo = orderNo;
            CustomerId = customerId;
            ItemDescription = itemDescription;
            Price = price;
            Status = OrderStatus.Requested;
            CreatedAt = DateTime.UtcNow;
        }

        public void Accept() => Transition(OrderStatus.Accepted, OrderStatus.Requested);
        public void MarkAsPaid() => Transition(OrderStatus.Paid, OrderStatus.Accepted);
        public void MarkAsPurchased() => Transition(OrderStatus.Purchased, OrderStatus.Paid);
        public void Ship() => Transition(OrderStatus.InTransit, OrderStatus.Purchased);
        public void MarkReadyForHandover() => Transition(OrderStatus.ReadyForHandover, OrderStatus.InTransit);
        public void Complete() => Transition(OrderStatus.Completed, OrderStatus.ReadyForHandover);

        public void Cancel()  // jalur tanpa uang: belum dibayar
        {
            EnsureStatusIn(OrderStatus.Requested, OrderStatus.Accepted);
            Status = OrderStatus.Cancelled;
        }

        public void Refund()  // jalur dengan uang: sudah dibayar/dibeli
        {
            EnsureStatusIn(OrderStatus.Paid, OrderStatus.Purchased);
            Status = OrderStatus.Refunded;
        }

        private void Transition(OrderStatus to, OrderStatus requiredFrom)
        {
            EnsureStatusIn(requiredFrom);
            Status = to;
        }

        private void EnsureStatusIn(params OrderStatus[] allowed)
        {
            if (!allowed.Contains(Status))
                throw new DomainException($"Transisi tidak valid dari status {Status}.");
        }
    }
}
