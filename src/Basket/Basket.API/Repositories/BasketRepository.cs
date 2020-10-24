using System.Threading.Tasks;
using Basket.API.Data.Interfaces;
using Basket.API.Models;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IBasketContext _context;
        public BasketRepository(IBasketContext context)
        {
            _context = context;

        }

        public async Task<BasketCartModel> GetBasket(string username)
        {
            var basket = await _context.Redis.StringGetAsync(username);

            if (basket.IsNullOrEmpty)
                return null;


            //convert json object from redis to object 
            return JsonConvert.DeserializeObject<BasketCartModel>(basket);

        }

        public async Task<BasketCartModel> UpdateBasket(BasketCartModel basketCart)
        {
            var result = await _context.Redis
                                .StringSetAsync(basketCart.Username, JsonConvert.SerializeObject(basketCart));

            if (!result)
                return null;

            
            return await GetBasket(basketCart.Username);

        }

        public async Task<bool> DeleteBasket(string username)
        {
            return await _context.Redis.KeyDeleteAsync(username);
        }

        
    }
}