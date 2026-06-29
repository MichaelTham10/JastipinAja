using System;
using System.Collections.Generic;
using System.Text;
using JastipinAja.BuildingBlocks.Generic;
using JastipinAja.Order.Persistence;
using JastipinAja.Order.Domain;

namespace JastipinAja.Order.Features.CreateOrder
{
    internal sealed class CreateOrderHandler
    {
        private readonly OrderDbContext _db;
        private readonly RunningNumberGenerator<OrderDbContext> _numbers;

        public CreateOrderHandler(OrderDbContext db, RunningNumberGenerator<OrderDbContext> numbers)
        {
            _db = db;
            _numbers = numbers;
        }

        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken ct)
        {
            var orderNo = await _numbers.NextAsync(
                new RunningNumberRequest("ordering.OrderNoSeq", "ORD"), ct);

            var order = new Domain.Order(command.CustomerId, orderNo, command.ItemDescription, command.Price);

            _db.Orders.Add(order);
            await _db.SaveChangesAsync(ct);

            return new CreateOrderResult(order.PublicId, order.OrderNo);
        }
    }
}
