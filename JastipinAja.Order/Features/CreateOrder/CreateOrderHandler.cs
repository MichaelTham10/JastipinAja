using FluentValidation;
using JastipinAja.BuildingBlocks.Generic;
using JastipinAja.Order.Domain;
using JastipinAja.Order.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace JastipinAja.Order.Features.CreateOrder
{
    internal sealed class CreateOrderHandler
    {
        private readonly OrderDbContext _db;
        private readonly RunningNumberGenerator<OrderDbContext> _numbers;
        private readonly IValidator<CreateOrderCommand> _validator;


        public CreateOrderHandler(OrderDbContext db, RunningNumberGenerator<OrderDbContext> numbers, IValidator<CreateOrderCommand> validator)
        {
            _db = db;
            _numbers = numbers;
            _validator = validator;
        }

        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken ct)
        {
            await _validator.ValidateAndThrowAsync(command, ct);  // gagal → ValidationException → 400

            var orderNo = await _numbers.NextAsync(
                new RunningNumberRequest("ordering.OrderNoSeq", "ORD"), ct);

            var order = new Domain.Order(command.CustomerId, orderNo, command.ItemDescription, command.Price);

            _db.Orders.Add(order);
            await _db.SaveChangesAsync(ct);

            return new CreateOrderResult(order.PublicId, order.OrderNo);
        }
    }
}
