using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Payment.Features.CreatePayment
{
    internal sealed class CreatePaymentValidation : AbstractValidator<CreatePaymentCommand>
    {
        public CreatePaymentValidation()
        {
            RuleFor(x => x.OrderPublicId)
                .NotEmpty().WithMessage("OrderPublicId wajib diisi.");
            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount harus lebih dari nol.");
        }
    }
}
