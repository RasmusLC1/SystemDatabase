using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Customers;

namespace CustomerDatabase.Tests
{
    public class CustomerHandlerTests
    {
        public CustomerHandlerTests()
        {
            // Clear the singleton's internal list before each test.
            var handler = CustomerHandler.Instance;
            var customersField = typeof(CustomerHandler)
                .GetField("customers", BindingFlags.NonPublic | BindingFlags.Instance);

            (customersField?.GetValue(handler) as List<Customer>)?.Clear();
        }

        

        [Fact]
        public void Singleton_Instance_IsNotNull()
        {
            // Act
            var instance = CustomerHandler.Instance;

            // Assert
            Assert.NotNull(instance);
        }

        [Fact]
        public void AddCustomer_IncreasesCustomerCount()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            var customer = new Customer("John Doe", new DateTime(1985, 5, 20), "123 Main St");

            // Assert
            Assert.Single(handler.GetCustomers()); // Expecting exactly one customer in the list
        }

        [Fact]
        public void ClearCustomers(){
            var handler = CustomerHandler.Instance;
            var customer = new Customer("John Doe", new DateTime(1985, 5, 20), "123 Main St");
            var customers = handler.GetCustomers();
            Assert.NotEmpty(customers);
            handler.ClearCustomers();
            Assert.Empty(customers);

        }

        [Fact]
        public void GetCustomers_ReturnsAllCustomers()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();
            var customer1 = new Customer("Alice Brown", new DateTime(1990, 3, 15), "456 Elm St");
            var customer2 = new Customer("Bob White", new DateTime(1982, 7, 22), "789 Oak St");

            // Act
            var customers = handler.GetCustomers();

            // Assert
            Assert.Equal(2, customers.Count);
            Assert.Contains(customer1, customers);
            Assert.Contains(customer2, customers);
        }

        [Fact]
        public void RemoveCustomer_DeletesExistingCustomer()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();
            Assert.Empty(handler.GetCustomers());
            var customer = new Customer("Chris Black", new DateTime(1987, 11, 5), "321 Pine St");

            // Act
            bool removed = handler.RemoveCustomer(customer.ID);
            // Assert
            Assert.True(removed);
            Assert.Empty(handler.GetCustomers()); // Should be empty after removal
        }

        [Fact]
        public void RemoveCustomer_ReturnsFalse_IfCustomerNotFound()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();

            // Act
            bool removed = handler.RemoveCustomer(999); // Non-existent ID

            // Assert
            Assert.False(removed);
        }

        [Fact]
        public void GetNextID_ReturnsIncrementedID()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();

            var customer1 = new Customer("Emily Green", new DateTime(1992, 6, 30), "567 Maple St");

            // Act
            int nextID = handler.GetNextID();

            // Assert
            Assert.Equal(customer1.ID + 1, nextID);
        }

        [Fact]
        public void GetNextID_ReturnsOne_WhenNoCustomersExist()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();


            // Act
            int nextID = handler.GetNextID();

            // Assert
            Assert.Equal(1, nextID);
        }

        [Fact]
        public void getIDByName_ReturnsCorrectID()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();

            var customer = new Customer("Olivia Martin", new DateTime(1995, 9, 10), "890 Cedar St");

            // Act
            int retrievedID = handler.getIDByName("Olivia Martin");

            // Assert
            Assert.Equal(customer.ID, retrievedID);
        }

        [Fact]
        public void getIDByName_ReturnsZero_WhenCustomerNotFound()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();

            // Act
            int retrievedID = handler.getIDByName("Nonexistent Name");

            // Assert
            Assert.Equal(0, retrievedID);
        }

        [Fact]
        public void PrintCustomer_ReturnsLabel_WhenCustomerExists()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();

            var customer = new Customer("Liam Jackson", new DateTime(1980, 12, 1), "777 Birch St");

            // Act
            var label = handler.PrintCustomer(customer.ID);

            // Assert
            Assert.NotNull(label);
            Assert.Contains($"Name: {customer.Name}", label.Text);
            Assert.Contains($"Age: {customer.GetAge()}", label.Text);
            Assert.Contains($"Address: {customer.Address}", label.Text);
        }

        [Fact]
        public void PrintCustomer_ReturnsNull_WhenCustomerNotFound()
        {
            // Arrange
            var handler = CustomerHandler.Instance;
            handler.ClearCustomers();


            // Act
            var label = handler.PrintCustomer(999); // Non-existent ID

            // Assert
            Assert.Null(label);
        }
    }
}
