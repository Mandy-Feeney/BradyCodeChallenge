namespace BradyCodeChallenge.Core.Models;

public class CustomerOrderSummary
{
    public int CustomerNumber { get; set; }
    public int NumberOfItems { get; set; }
    public decimal TotalCost { get; set; }
}
