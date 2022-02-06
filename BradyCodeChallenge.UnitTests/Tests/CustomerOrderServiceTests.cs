using BradyCodeChallenge.Core.Models;
using BradyCodeChallenge.Core.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BradyCodeChallenge.UnitTests.Tests;

public class CustomerOrderServiceTests
{
    [Fact]
    public void GenerateCustomerOrderSummaries_SingleCustomer_ReturnsOrderSummaryForCustomer()
    {
        // Arrange
        Order order = new Order()
        {
            OrderNumber = 1,
            OrderDetails = new()
            {
                new OrderDetail()
                {
                    ItemNumber = 1,
                    CustomerNumber = 1,
                    OrderDate = DateTime.Now,
                    Quantity = 10,
                    Cost = 2.00M
                },
                new OrderDetail()
                {
                    ItemNumber = 1,
                    CustomerNumber = 1,
                    OrderDate = DateTime.Now,
                    Quantity = 2,
                    Cost = 5.00M
                }
            }
        };

        int expectedNumberOfItems = 12;
        decimal expectedTotalCost = 30.00M;

        CustomerOrderService customerOrderService = new(order);

        // Act
        List<CustomerOrderSummary> customerOrderSummary = customerOrderService.GenerateCustomerOrderSummaries();

        // Assert
        customerOrderSummary.Should().HaveCount(1);
        customerOrderSummary.First().NumberOfItems.Should().Be(expectedNumberOfItems);
        customerOrderSummary.First().TotalCost.Should().Be(expectedTotalCost);
    }

    [Fact]
    public void GenerateCustomerOrderSummaries_MultipleCustomers_ReturnsOrderSummaryForEachCustomer()
    {
        // Arrange
        Order order = new Order()
        {
            OrderNumber = 1,
            OrderDetails = new()
            {
                new OrderDetail()
                {
                    ItemNumber = 1,
                    CustomerNumber = 1,
                    OrderDate = DateTime.Now,
                    Quantity = 3,
                    Cost = 1.50M
                },
                new OrderDetail()
                {
                    ItemNumber = 1,
                    CustomerNumber = 2,
                    OrderDate = DateTime.Now,
                    Quantity = 8,
                    Cost = 4.00M
                },
                new OrderDetail()
                {
                    ItemNumber = 1,
                    CustomerNumber = 2,
                    OrderDate = DateTime.Now,
                    Quantity = 5,
                    Cost = 10.00M
                }
            }
        };

        int customerOneExpectedNumberOfItems = 3;
        decimal customerOneExpectedTotalCost = 4.50M;
        int customerTwoExpectedNumberOfItems = 13;
        decimal customerTwoExpectedTotalCost = 82.00M;

        CustomerOrderService customerOrderService = new(order);

        // Act
        List<CustomerOrderSummary> customerOrderSummary = customerOrderService.GenerateCustomerOrderSummaries();

        // Assert
        customerOrderSummary.Should().HaveCount(2);

        CustomerOrderSummary customerOneSummary = customerOrderSummary.First(summary => summary.CustomerNumber.Equals(1));
        CustomerOrderSummary customerTwoSummary = customerOrderSummary.First(summary => summary.CustomerNumber.Equals(2));

        customerOneSummary.NumberOfItems.Should().Be(customerOneExpectedNumberOfItems);
        customerOneSummary.TotalCost.Should().Be(customerOneExpectedTotalCost);

        customerTwoSummary.NumberOfItems.Should().Be(customerTwoExpectedNumberOfItems);
        customerTwoSummary.TotalCost.Should().Be(customerTwoExpectedTotalCost);
    }
}

