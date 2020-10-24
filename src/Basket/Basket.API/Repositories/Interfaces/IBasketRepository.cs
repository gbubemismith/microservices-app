using System.Threading.Tasks;
using Basket.API.Models;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
         Task<BasketCartModel> GetBasket(string username);
         Task<BasketCartModel> UpdateBasket(BasketCartModel basketCart);
         Task<bool> DeleteBasket(string username);
    }
}