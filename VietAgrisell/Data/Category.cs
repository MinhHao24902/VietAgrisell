using System;
using System.Collections.Generic;

namespace VietAgrisell.Data;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryImageUrl { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
