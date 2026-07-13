using JastipinAja.BuildingBlocks.Exceptions;

namespace JastipinAja.Payment.Domain
{
    internal sealed class Payment
    {
        public long Id { get; private set; }
        public Guid PublicId { get; private set; }
        public string PaymentNo { get; private set; } = string.Empty;
        public Guid OrderPublicId { get; private set; }   // referensi ke Order, lewat PublicId (bukan FK internal)
        public decimal Amount { get; private set; }
        public PaymentStatus Status { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Payment() { }

        public Payment(Guid orderPublicId, string paymentNo, decimal amount)
        {
            if (orderPublicId == Guid.Empty)
                throw new DomainException("OrderPublicId wajib diisi.");
            if (amount <= 0)
                throw new DomainException("Amount harus lebih dari nol.");

            PublicId = Guid.NewGuid();
            PaymentNo = paymentNo;
            OrderPublicId = orderPublicId;
            Amount = amount;
            Status = PaymentStatus.Held;
            CreatedAt = DateTime.UtcNow;
        }

        public void Capture() => Transition(PaymentStatus.Captured, PaymentStatus.Held);
        public void Release() => Transition(PaymentStatus.Released, PaymentStatus.Captured);

        public void Refund()
        {
            EnsureStatusIn(PaymentStatus.Held, PaymentStatus.Captured);
            Status = PaymentStatus.Refunded;
        }

        private void Transition(PaymentStatus to, PaymentStatus requiredFrom)
        {
            EnsureStatusIn(requiredFrom);
            Status = to;
        }

        private void EnsureStatusIn(params PaymentStatus[] allowed)
        {
            if (!allowed.Contains(Status))
                throw new DomainException($"Transisi pembayaran tidak valid dari status {Status}.");
        }
    }
}
