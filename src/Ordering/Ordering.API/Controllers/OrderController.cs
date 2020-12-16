using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Commands;
using Ordering.Application.Queries;

namespace Ordering.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediatr;

        public OrderController(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersByUsername(string username)
        {
            var query = new GetOrderByUsernameQuery(username);

            var orders = await _mediatr.Send(query);

            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutOrder(CheckoutOrderCommand command)
        {
            var result = await _mediatr.Send(command);

            return Ok(result);
        }
    }
}