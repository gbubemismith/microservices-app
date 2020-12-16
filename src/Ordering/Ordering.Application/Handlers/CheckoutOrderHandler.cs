using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ordering.Application.Commands;
using Ordering.Application.Helpers;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class CheckoutOrderHandler : IRequestHandler<CheckoutOrderCommand, OrderResponseDto>
    {
        private readonly IOrderRepository _orderRepository;

        public CheckoutOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<OrderResponseDto> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = OrderMapper.Mapper.Map<Order>(request);

            if (orderEntity == null)
                throw new ApplicationException("not mapped");

            var newOrder = await _orderRepository.AddAsync(orderEntity);

            var orderResponse = OrderMapper.Mapper.Map<OrderResponseDto>(newOrder);

            return orderResponse;
        }
    }
}