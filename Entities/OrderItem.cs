using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MyFirstWebApiSite;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public int Quantitiy { get; set; }

    public virtual Order Order { get; set; } = null!;
 
    public virtual Product Product { get; set; } = null!;
}
