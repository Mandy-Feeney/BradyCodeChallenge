using BradyCodeChallenge.Core.IO;
using BradyCodeChallenge.Core.Models;
using BradyCodeChallenge.Core.Services;
using System.Configuration;

Console.WriteLine("Brady Code Challenge\n");

using FileSystemWatcher jsonOrdersFileWatcher = new FileSystemWatcher()
{
    Path = ConfigurationManager.AppSettings.Get("JsonInputDirectory"),
    Filter = "*.json"
};

jsonOrdersFileWatcher.Created += new FileSystemEventHandler(ProcessJsonOrders);
jsonOrdersFileWatcher.EnableRaisingEvents = true;

Console.ReadLine();

void ProcessJsonOrders(object sender, FileSystemEventArgs e)
{
    Console.WriteLine("Detected JSON file with orders to process\n");

    OrderSerializer orderSerializer = new();
    Order order = orderSerializer.DeserializeRawOrder(e.FullPath);

    string outputJsonFile = Path.Combine(Directory.GetCurrentDirectory(), "ValidOrders.json");
    orderSerializer.SerializeToFile(order, outputJsonFile);

    order = orderSerializer.Deserialize(outputJsonFile);

    CustomerOrderReporter reporter = new(new CustomerOrderService(order));
    reporter.GenerateSummaryReport(ConfigurationManager.AppSettings.Get("JsonOutputDirectory"));

    Console.WriteLine($"Generated Customer Order Summary report: {ConfigurationManager.AppSettings.Get("JsonOutputDirectory")}\n");
}