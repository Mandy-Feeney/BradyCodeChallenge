namespace BradyCodeChallenge.Core.Models;

public class DeserializableOrderDetail
{
    public string ItemNumber { get; set; }
    public string CustomerNumber { get; set; }
    public string OrderDate { get; set; }
    public string Quantity { get; set; }
    public string Cost { get; set; }
}