using System.Threading.Tasks;
using Basket.API.Models;
using Basket.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepo;

        public BasketController(IBasketRepository basketRepo)
        {
            _basketRepo = basketRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string username)
        {
            var basket = await _basketRepo.GetBasket(username);

            return Ok(basket);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateBasket(BasketCartModel basketCart)
        {
            var result = await _basketRepo.UpdateBasket(basketCart);

            return Ok(result);
        }

        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            var result = await _basketRepo.DeleteBasket(username);

            return Ok(result);
        }

    }
}