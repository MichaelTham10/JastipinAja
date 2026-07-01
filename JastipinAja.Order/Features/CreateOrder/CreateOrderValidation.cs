using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.CreateOrder
{
    internal sealed class CreateOrderValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId wajib diisi.");

            RuleFor(x => x.ItemDescription)
                .NotEmpty().WithMessage("Deskripsi barang wajib diisi.")
                .MaximumLength(500).WithMessage("Deskripsi maksimal 500 karakter.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Harga harus lebih dari nol.");
        }
    }
}
