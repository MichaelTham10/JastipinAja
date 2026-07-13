using FluentValidation;
using JastipinAja.BuildingBlocks.Generic;
using JastipinAja.Payment.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.CreatePayment
{
    internal class CreatePaymentHandler
    {
        private readonly PaymentDbContext _db;
        private readonly RunningNumberGenerator<PaymentDbContext> _numbers;
        private readonly IValidator<CreatePaymentCommand> _validator;

        public CreatePaymentHandler(PaymentDbContext db, RunningNumberGenerator<PaymentDbContext> numbers, IValidator<CreatePaymentCommand> validator)
        {
            _db = db;
            _numbers = numbers;
            _validator = validator;
        }

        public async Task<CreatePaymentResult> Handle(CreatePaymentCommand command, CancellationToken ct)
        {
            await _validator.ValidateAndThrowAsync(command, ct);  // gagal → ValidationException → 400

            var paymentNo = await _numbers.NextAsync(
                new RunningNumberRequest("payment.\"PaymentNoSeq\"", "PAY"), ct);

            var paymentObj = new Domain.Payment(command.OrderPublicId, paymentNo, command.Amount);

            _db.Payments.Add(paymentObj);
            await _db.SaveChangesAsync(ct);

            return new CreatePaymentResult(paymentObj.PublicId, paymentObj.PaymentNo);
        }
    }
}
