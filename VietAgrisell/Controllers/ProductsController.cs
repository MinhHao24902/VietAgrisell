using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using VietAgrisell.Data;
using VietAgrisell.ViewModels;

namespace VietAgrisell.Controllers
{
    public class ProductsController : Controller
    {
        private readonly VietAgrisellContext db;

        public ProductsController(VietAgrisellContext context)
        {
            db = context;
        }
        public IActionResult Index(int? Cate)
        {
            var products = db.Products.AsQueryable();

            if (Cate.HasValue)
            {
                products = products.Where(p => p.CategoryId == Cate.Value);
            }

            var result = products.Select(p => new ProductsViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ImageUrl = p.ImageUrl ?? "",
                Price = p.Price,
                Unit = p.Unit,
                ProductDescription = p.ShortDescription ?? "",
                ProductCategory = p.Category.CategoryName,
            });

            return View(result);
        }

        public IActionResult Search(string? Proname)
        {
            var products = db.Products.AsQueryable();

            if (Proname != null)
            {
                products = products.Where(p => p.ProductName.Contains(Proname));
            }

            var result = products.Select(p => new ProductsViewModel
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ImageUrl = p.ImageUrl ?? "",
                Price = p.Price,
                Unit = p.Unit,
                ProductDescription = p.ShortDescription ?? "",
                ProductCategory = p.Category.CategoryName,
            });

            return View(result);
        }

        public IActionResult Detail(int id)
        {
            var data = db.Products
                .Include(p => p.Category)
                .SingleOrDefault(p => p.ProductId == id);

            if(data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var result = new ProductDetailViewModel
            {
                ProductId = data.ProductId,
                ProductName = data.ProductName,
                ImageUrl = data.ImageUrl ?? "",
                Price = data.Price,
                ProductDescription = data.ShortDescription ?? string.Empty,
                ProductCategory = data.Category.CategoryName,
                Quantity = data.Quantity,
                Point = data.Point,
            };
            return View(result);
        }
    }
}
