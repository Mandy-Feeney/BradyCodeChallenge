using BradyCodeChallenge.Core.Models;

namespace BradyCodeChallenge.Core.Services;

public class CustomerOrderService
{
    private readonly Order order;

    public CustomerOrderService(Order order)
    {
        this.order = order;
    }

    public List<CustomerOrderSummary> GenerateCustomerOrderSummaries()
    {
        List<CustomerOrderSummary> customerOrderSummaries = new();
        var groupedCustomerOrders = order.OrderDetails.GroupBy(o => o.CustomerNumber);

        foreach (var customerGroup in groupedCustomerOrders)
        {
            int totalItems = 0;
            List<decimal> orderTotals = new();

            foreach (var customerOrder in customerGroup)
            {
                totalItems += customerOrder.Quantity;
                orderTotals.Add(CalculateTotalPriceOfOrder(customerOrder));
            }

            customerOrderSummaries.Add(new CustomerOrderSummary()
            {
                CustomerNumber = customerGroup.Key,
                NumberOfItems = totalItems,
                TotalCost = orderTotals.Sum()
            });
        }

        return customerOrderSummaries;
    }

    private decimal CalculateTotalPriceOfOrder(OrderDetail orderDetail)
    {
        return orderDetail.Quantity * orderDetail.Cost;
    }
}

