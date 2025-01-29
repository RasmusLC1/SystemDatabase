using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Tickets;
using Employees;
using Customers;

namespace TicketDatabase.Tests
{
    public class TicketTests
    {
        public TicketTests()
        {
            // Clear the singleton instances before each test
            ClearSingletonInstance(typeof(TicketHandler), "tickets");
            ClearSingletonInstance(typeof(EmployeeHandler), "employees");
        }

        private void ClearSingletonInstance(Type type, string fieldName)
        {
            var handler = type
                .GetProperty("Instance", BindingFlags.Public | BindingFlags.Static)?
                .GetValue(null);

            var field = type.GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance);
            (field?.GetValue(handler) as IList<Ticket>)?.Clear();
        }

        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int customerID = 100;
            int price = 250;

            // Act
            var ticket = new Ticket(customerID, price);

            // Assert
            Assert.Equal(customerID, ticket.CustomerID);
            Assert.Equal(price, ticket.Price);
            Assert.Null(ticket.Completed); // Should not be completed initially
            Assert.NotEqual(0, ticket.ID); // ID should be assigned from singleton
        }

        [Fact]
        public void GetAge_CalculatesCorrectly()
        {
            // Arrange
            var ticket = new Ticket(1, 100);

            // Act
            int age = ticket.GetAge();

            // Assert
            int expectedAge = DateTime.Now.Day - ticket.Creation.Day;
            Assert.Equal(expectedAge, age);
        }

        [Fact]
        public void SetComplete_SetsCompletedDate()
        {
            // Arrange
            var ticket = new Ticket(1, 100);

            // Act
            ticket.SetComplete();

            // Assert
            Assert.NotNull(ticket.Completed);
            Assert.True(ticket.Completed <= DateTime.Now);
        }
        [Fact]
        public void AssignEmployeeToTicket(){
            var ticket = new Ticket(1);

        }

        [Fact]
        public void ReturnTicketInfo_SetsLabelTextCorrectly()
        {
            // Arrange
            var employeeHandler = EmployeeHandler.Instance;
            Employee employee = new Employee("John Doe", new DateTime(1980, 1, 1), "Developer", "City", 50000);
            Customer customer = new Customer("Test User", new DateTime(1985, 4, 2), "Test Address");
            var ticket = new Ticket(customer.ID, 2500);
            employee.AddTicket(ticket.ID);

            // Act
            Label label = ticket.ReturnTicketInfo();

            // Assert
            Assert.NotNull(label);
            Assert.Contains($"ID: {ticket.ID}", label.Text);
            Assert.Contains($"Employee: {employee.Name}", label.Text);
            Assert.Contains($"Customer: {customer.Name}", label.Text);
            Assert.Contains($"Price: {ticket.Price}", label.Text);
        }
    }
}
