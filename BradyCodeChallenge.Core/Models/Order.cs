namespace BradyCodeChallenge.Core.Models;

public class Order
{
    public int OrderNumber { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
}

