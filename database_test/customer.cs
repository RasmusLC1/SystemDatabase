using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Customers;

namespace CustomerDatabase.Tests
{
    public class CustomerTests
    {
        public CustomerTests()
        {
            // Clear the internal list of the singleton before each test.
            var handler = CustomerHandler.Instance;
            var customersField = typeof(CustomerHandler)
                .GetField("customers", BindingFlags.NonPublic | BindingFlags.Instance);

            (customersField?.GetValue(handler) as List<Customer>)?.Clear();
        }

        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            string name = "Jane Doe";
            DateTime birthday = new DateTime(1990, 10, 15);
            string address = "123 Main St, Seattle";

            // Act
            var customer = new Customer(name, birthday, address);

            // Assert
            Assert.Equal(name, customer.Name);
            Assert.Equal(birthday, customer.Birthday);
            Assert.Equal(address, customer.Address);
            Assert.NotEqual(0, customer.ID); // ID should be assigned from the singleton
        }

        [Fact]
        public void GetAge_CalculatesCorrectly()
        {
            // Arrange
            DateTime birthday = new DateTime(2000, 1, 1);
            var customer = new Customer("Test User", birthday, "Test Address");

            // Act
            int age = customer.GetAge();

            // Assert
            int expectedAge = DateTime.Now.Year - 2000;
            if (DateTime.Now < birthday.AddYears(expectedAge))
            {
                expectedAge--;
            }
            Assert.Equal(expectedAge, age);
        }

        [Fact]
        public void AddTicket_IncreasesTicketCount()
        {
            // Arrange
            var customer = new Customer("John Smith", new DateTime(1995, 5, 25), "456 Elm St");
            int ticketID1 = 1001;
            int ticketID2 = 1002;

            // Act
            customer.AddTicket(ticketID1);
            customer.AddTicket(ticketID2);

            // Use Reflection to access the private `tickets` field
            var ticketsField = typeof(Customer).GetField("tickets", BindingFlags.NonPublic | BindingFlags.Instance);
            var tickets = ticketsField?.GetValue(customer) as List<int>;

            // Assert
            Assert.NotNull(tickets);
            Assert.Equal(2, tickets.Count);
            Assert.Contains(ticketID1, tickets);
            Assert.Contains(ticketID2, tickets);
        }

        [Fact]
        public void ReturnCustomerInfo_SetsLabelTextCorrectly()
        {
            // Arrange
            var customer = new Customer("Alice Johnson", new DateTime(1988, 6, 12), "789 Pine St");
            customer.AddTicket(2001);
            customer.AddTicket(2002);

            // Act
            Label label = customer.ReturnCustomerInfo();

            // Assert
            Assert.NotNull(label);
            Assert.Contains($"ID: {customer.ID}", label.Text);
            Assert.Contains($"Name: {customer.Name}", label.Text);
            Assert.Contains($"Age: {customer.GetAge()}", label.Text);
            Assert.Contains($"Address: {customer.Address}", label.Text);
            Assert.Contains($"Tickets count: 2", label.Text); // 2 tickets added
            Assert.Equal("Arial", label.Font.Name);
            Assert.True(label.AutoSize);
        }
    }
}
