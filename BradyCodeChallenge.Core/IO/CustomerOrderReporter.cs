using BradyCodeChallenge.Core.Models;
using BradyCodeChallenge.Core.Services;
using System.Text.Json;

namespace BradyCodeChallenge.Core.IO;

public class CustomerOrderReporter
{
    private readonly CustomerOrderService customerOrderService;

    public CustomerOrderReporter(CustomerOrderService customerOrderService)
    {
        this.customerOrderService = customerOrderService;
    }

    public void GenerateSummaryReport(string outputFilePath)
    {
        List<CustomerOrderSummary> customerOrderSummaries = customerOrderService.GenerateCustomerOrderSummaries();

        string orderAsString = JsonSerializer.Serialize(customerOrderSummaries);
        string fullOutputPath = Path.Combine(outputFilePath, $"{DateTime.Now.ToString("yyyy-dd-M--HH-mm-ss")}.json");
        File.WriteAllText(fullOutputPath, orderAsString);
    }
}
