using Microsoft.AspNetCore.Mvc;
using VietAgrisell.Helpers;
using VietAgrisell.ViewModels;

namespace VietAgrisell.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModel
            {
                Quantity = cart.Sum(x => x.Quantity),
                Total = cart.Sum(x => x.TotalPrice)
            });
        }
    }
}
