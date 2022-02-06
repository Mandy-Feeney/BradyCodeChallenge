using BradyCodeChallenge.Core.Extensions;
using BradyCodeChallenge.Core.Models;
using System.Text.Json;

namespace BradyCodeChallenge.Core.IO;

public class OrderSerializer
{
    public void SerializeToFile(Order order, string outputPath)
    {
        string orderAsString = JsonSerializer.Serialize(order);
        File.WriteAllText(outputPath, orderAsString);
    }

    public Order DeserializeRawOrder(string pathToJsonFile)
    {
        string orderAsString = File.ReadAllText(pathToJsonFile);

        DeserializableOrder jsonOrder = JsonSerializer.Deserialize<DeserializableOrder>(orderAsString);
        return jsonOrder.ConvertOrder();
    }

    public Order Deserialize(string pathToJsonFile)
    {
        string orderAsString = File.ReadAllText(pathToJsonFile);
        return JsonSerializer.Deserialize<Order>(orderAsString);
    }
}
