using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Tickets;
using Employees;

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
            int employeeID = 1;
            int customerID = 100;
            int price = 250;

            // Act
            var ticket = new Ticket(employeeID, customerID, price);

            // Assert
            Assert.Equal(employeeID, ticket.EmployeeID);
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
        public void ReturnTicketInfo_SetsLabelTextCorrectly()
        {
            // Arrange
            var employeeHandler = EmployeeHandler.Instance;
            new Employee("John Doe", new DateTime(1980, 1, 1), "Developer", "City", 50000);

            var ticket = new Ticket(1, 100);

            // Act
            Label label = ticket.ReturnTicketInfo();

            // Assert
            Assert.NotNull(label);
            Assert.Contains($"ID: {ticket.ID}", label.Text);
            Assert.Contains($"Employee: John Doe", label.Text);
            Assert.Contains($"Age: {ticket.GetAge()}", label.Text);
        }
    }
}
