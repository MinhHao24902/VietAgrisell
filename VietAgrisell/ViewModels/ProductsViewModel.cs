using System;

namespace VietAgrisell.ViewModels
{
    public class ProductsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Unit {  get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
    }

    public class ProductDetailViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public int Point { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCategory { get; set; }
        public int Quantity { get; set; }
    }
}
