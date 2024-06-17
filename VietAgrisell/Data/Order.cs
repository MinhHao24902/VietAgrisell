using System;
using System.Collections.Generic;

namespace VietAgrisell.Data;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? DateToBeDelivered { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public string? CustomerName { get; set; }

    public string Address { get; set; } = null!;

    public string Mobile { get; set; } = null!;

    public string PaymentMethod { get; set; } = null!;

    public string ShippingWay { get; set; } = null!;

    public decimal ShippingFee { get; set; }

    public int? Status { get; set; }

    public string? Note { get; set; }
}
