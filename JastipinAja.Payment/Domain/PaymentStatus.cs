namespace JastipinAja.Payment.Domain
{
    public enum PaymentStatus
    {
        Created,
        Held,
        Captured,
        Released,
        Refunded
    }
}