using Microsoft.AspNetCore.Mvc;
using VietAgrisell.ViewModels;
using VietAgrisell.Data;

namespace VietAgrisell.ViewComponents
{
    public class CategoriesMenuViewComponent : ViewComponent
    {
        private readonly VietAgrisellContext db;

        public CategoriesMenuViewComponent(VietAgrisellContext context) => db = context;

        public IViewComponentResult Invoke()
        {
            var data = db.Categories.Select(cate => new CategoriesMenuViewModel
            {
                CategoryId = cate.CategoryId,
                CategoryName = cate.CategoryName,
                ImageUrl = cate.CategoryImageUrl,
                Amount = cate.Products.Count()
            }).OrderBy(p => p.CategoryName);

            return View(data);
        }
    }
}
