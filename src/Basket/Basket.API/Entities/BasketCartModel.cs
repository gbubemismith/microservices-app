using System.Collections.Generic;

namespace Basket.API.Entities
{
    public class BasketCartModel
    {
        public BasketCartModel()
        {
            Items = new List<BasketCartItem>();
        }
        public BasketCartModel(string username)
        {
            Username = username;
            Items = new List<BasketCartItem>();
        }
        public string Username { get; set; }
        public List<BasketCartItem> Items { get; set; }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                foreach (var item in Items)
                {
                    totalPrice += item.Price * item.Quantity;
                }

                return totalPrice;
            }

        }
    }
}