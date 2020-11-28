using System.Collections;
using System.Collections.Generic;
using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public class GetOrderByUsernameQuery : IRequest<IEnumerable<OrderResponseDto>>
    {
        public string Username { get; set; }

        public GetOrderByUsernameQuery(string username)
        {
            Username = username;

        }
    }
}