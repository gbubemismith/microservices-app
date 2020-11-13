using System;
using System.Threading.Tasks;
using AutoMapper;
using Basket.API.Models;
using Basket.API.Repositories.Interfaces;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Helpers;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IMapper _mapper;
        private readonly EventBusRabbitMqProducer _eventBus;

        public BasketController(IBasketRepository basketRepo, IMapper mapper, EventBusRabbitMqProducer eventBus)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
            _eventBus = eventBus;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket(string username)
        {
            var basket = await _basketRepo.GetBasket(username);

            if (basket == null)
                return Ok(new BasketCartModel(username));

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

        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout(BasketCheckoutModel basketCheckout)
        {
            
            //get total price of basket
            var basket = await _basketRepo.GetBasket(basketCheckout.Username);

            if (basket == null)
                return BadRequest();

            //remove the basket
            var basketRemoved = await _basketRepo.DeleteBasket(basketCheckout.Username);

            if (!basketRemoved)
                return BadRequest();
            
            //send checkout event to rabbitMq
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.RequestId = Guid.NewGuid();
            eventMessage.TotalPrice = basket.TotalPrice;

            try
            {
                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, eventMessage);
            }
            catch (System.Exception)
            {
                
                throw;
            }

            return Accepted();
        }

    }
}