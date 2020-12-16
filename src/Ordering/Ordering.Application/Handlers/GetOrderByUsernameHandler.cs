using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Ordering.Application.Helpers;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Repositories;

namespace Ordering.Application.Handlers
{
    public class GetOrderByUsernameHandler : IRequestHandler<GetOrderByUsernameQuery, IEnumerable<OrderResponseDto>>
    {
        private readonly IOrderRepository _orderRepository;
        public GetOrderByUsernameHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;

        }

        public async Task<IEnumerable<OrderResponseDto>> Handle(GetOrderByUsernameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrderByUserName(request.Username);

            var orderResponseList = OrderMapper.Mapper.Map<IEnumerable<OrderResponseDto>>(orderList);

            return orderResponseList;
        }
    }
}