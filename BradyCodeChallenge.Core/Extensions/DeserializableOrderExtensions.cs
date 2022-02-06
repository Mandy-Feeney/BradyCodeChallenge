using BradyCodeChallenge.Core.Models;
using System.Text.Json;

namespace BradyCodeChallenge.Core.Extensions;

public static class DeserializableOrderExtensions
{
    public static Order ConvertOrder(this DeserializableOrder order)
    {
        List<OrderDetail> convertedOrderDetails = new();

        order.ReportAnyErrors();
        order.OrderDetails.ForEach(orderDetail => { convertedOrderDetails.Add(orderDetail.ConvertOrderDetail()); });

        return new Order()
        { 
            OrderNumber = int.Parse(order.OrderNumber),  
            OrderDetails = convertedOrderDetails.Where(o => o != null).ToList() 
        };
    }

    public static OrderDetail ConvertOrderDetail(this DeserializableOrderDetail orderDetail)
    {
        OrderDetail convertedOrderDetail = new();
        List<bool> validOrderDetails = new();

        validOrderDetails.Add(int.TryParse(orderDetail.ItemNumber, out int itemNumberResult));
        validOrderDetails.Add(int.TryParse(orderDetail.CustomerNumber, out int customerNumberResult));
        validOrderDetails.Add(DateTime.TryParse(orderDetail.OrderDate, out DateTime orderDateResult));
        validOrderDetails.Add(int.TryParse(orderDetail.Quantity, out int quantityResult));
        validOrderDetails.Add(decimal.TryParse(orderDetail.Cost, out decimal costResult));

        if(validOrderDetails.All(validOrderDetail => validOrderDetail.Equals(true)))
        {
            return new OrderDetail()
            {
                ItemNumber = itemNumberResult,
                CustomerNumber = customerNumberResult,
                OrderDate = orderDateResult,
                Quantity = quantityResult,
                Cost = costResult
            };
        }

        return null;
    }

    public static void ReportAnyErrors(this DeserializableOrder order)
    {
        List<string> errorMessages = GetErrorMessages(order);

        if(errorMessages.Any())
        {
            InvalidOrder invalidOrder = new InvalidOrder()
            {
                OrderNumber = order.OrderNumber,
                ErrorMessages = errorMessages
            };

            string invalidOrders = JsonSerializer.Serialize(invalidOrder);
            string fullOutputPath = Path.Combine(Directory.GetCurrentDirectory(), "Errors.json");
            File.WriteAllText(fullOutputPath, invalidOrders);
        }
    }

    private static List<string> GetErrorMessages(DeserializableOrder order)
    {
        List<string> errorMessages = new();

        order.OrderDetails.ForEach(orderDetail =>
        {
            bool canParse = int.TryParse(orderDetail.ItemNumber, out int itemNumberResult);
            if (!canParse)
                errorMessages.Add($"{orderDetail.ItemNumber} is not a valid integer value");

            canParse = int.TryParse(orderDetail.CustomerNumber, out int customerNumberResult);
            if (!canParse)
                errorMessages.Add($"{orderDetail.CustomerNumber} is not a valid integer value");

            canParse = DateTime.TryParse(orderDetail.OrderDate, out DateTime orderDateResult);
            if (!canParse)
                errorMessages.Add($"{orderDetail.OrderDate} is not a valid Date value");

            canParse = int.TryParse(orderDetail.Quantity, out int quantityResult);
            if (!canParse)
                errorMessages.Add($"{orderDetail.Quantity} is not a valid integer value");

            canParse = decimal.TryParse(orderDetail.Cost, out decimal costResult);
            if (!canParse)
                errorMessages.Add($"{orderDetail.Cost} is not a valid decimal value");
        });

        return errorMessages;
    }
}
