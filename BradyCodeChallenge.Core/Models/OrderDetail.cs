namespace BradyCodeChallenge.Core.Models;

public class OrderDetail
{
    public int ItemNumber { get; set; }
    public int CustomerNumber { get; set; }
    public DateTime OrderDate { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
}
