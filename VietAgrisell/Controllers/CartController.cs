using Microsoft.AspNetCore.Mvc;
using VietAgrisell.Data;
using VietAgrisell.ViewModels;
using VietAgrisell.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace VietAgrisell.Controllers
{
    public class CartController : Controller
    {
        private readonly VietAgrisellContext db;

        public CartController(VietAgrisellContext context) {
            db = context;
        }

        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new 
            List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductId == id);
            if(item == null)
            {
                var product = db.Products.SingleOrDefault(p => p.ProductId == id);
                if(product == null)
                {
                    TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    ProductPrice = product.Price,
                    ImageUrl = product.ImageUrl ?? string.Empty,
                    Quantity = quantity
                };
                cart.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, cart);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveCart(int id) 
        {
            var cart = Cart;
            var item = cart.SingleOrDefault(p => p.ProductId == id);
            if(item != null) 
            {
                cart.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, cart);
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        [HttpGet]
        public IActionResult Checkout ()
        {
            if(Cart.Count == 0)
            {
                return Redirect("/");
            }
            return View(Cart);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Checkout(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customerName = HttpContext.User.Claims.SingleOrDefault(p => p.Type == MySetting.CLAIM_CUSTOMERNAME).Value;
                var customer = new User();
                if(model.Receiver)
                {
                    customer = db.Users.SingleOrDefault(u => u.UserName == customerName);
                }

                var order = new Order
                {
                    UserId = model.UserId != 0 ? model.UserId : customer.UserId,
                CustomerName = model.CustomerName ?? customer.Name,
                    Address = model.Address ?? customer.Address,
                    Mobile = model.Mobile ?? customer.Mobile,
                    OrderDate = DateTime.Now,
                    PaymentMethod = "COD",
                    ShippingWay = "Shopee",
                    Status = 0,
                    Note = model.Note
                };

                db.Database.BeginTransaction();

                try
                {
                    db.Database.CommitTransaction();
                    db.Add(order);
                    db.SaveChanges();

                    var od = new List<OrdersDetail>();
                    foreach(var item in Cart)
                    {
                        od.Add(new OrdersDetail
                        {
                            OrderId = order.OrderId,
                            Quantity = item.Quantity,
                            Price = item.ProductPrice,
                            Discount = 0
                        });
                    }
                    db.AddRange(od);
                    db.SaveChanges();

                    HttpContext.Session.Set<List<CartItem>>(MySetting.CART_KEY, new List<CartItem>());
                    return View("Success");
                }
                catch
                {
                    db.Database.RollbackTransaction();
                }
            }
            else
            {
                ModelState.AddModelError("New Error", "Invalid Data");
            }
            return View(Cart);
        }
    }
}
