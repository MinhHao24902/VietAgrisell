using System;
using System.Collections.Generic;

namespace VietAgrisell.Data;

public partial class OrdersDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public decimal Discount { get; set; }
}
