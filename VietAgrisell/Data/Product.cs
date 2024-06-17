using System;
using System.Collections.Generic;

namespace VietAgrisell.Data;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public string Unit { get; set; } = null!;

    public int Point { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual Category Category { get; set; } = null!;
}
