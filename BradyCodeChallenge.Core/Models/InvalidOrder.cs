namespace BradyCodeChallenge.Core.Models;

public class InvalidOrder
{
    public string OrderNumber { get; set; }
    public List<string> ErrorMessages { get; set; }
}