namespace BradyCodeChallenge.Core.Models;

public class DeserializableOrder
{
    public string OrderNumber { get; set; }
    public List<DeserializableOrderDetail> OrderDetails { get; set; }
}