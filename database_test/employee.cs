using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Employees;
using Tickets;
using Customers;

namespace EmployeeDatabase.Tests
{
    public class EmployeeTests
    {

        public EmployeeTests()
        {
            // Clear the internal list of the singleton before each test.
            var handler = EmployeeHandler.Instance;
            var employeesField = typeof(EmployeeHandler)
                .GetField("employees", BindingFlags.NonPublic | BindingFlags.Instance);

            (employeesField?.GetValue(handler) as List<Employee>)?.Clear();
        }

         [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            string name = " John Doe  ";
            DateTime birthday = new DateTime(1985, 5, 20);
            string position = "Developer";
            string city = "Seattle";
            int salary = 75000;

            // Act
            var employee = new Employee(name, birthday, position, city, salary);

            // Assert
            Assert.Equal("John Doe", employee.Name);  
            Assert.Equal(birthday, employee.Birthday);
            Assert.Equal(position, employee.Position);
            Assert.Equal(city, employee.City);
            Assert.Equal(salary, employee.Salary);
            
            // We expect the ID to be non-zero if EmployeeHandler is empty before creation.
            // By default, GetNextID() returns 1 for the first Employee.
            Assert.NotEqual(0, employee.ID);
        }

        [Theory]
        [InlineData("Alice Brown", "AlBr")]
        [InlineData("Bob", "Bo")]     // Single-name edge case
        [InlineData("Chris T", "ChT")]// Last name with 1 letter
        public void CreateUserName_ReturnsExpected(string fullName, string expectedStart)
        {
            // Arrange
            var employee = new Employee(fullName, DateTime.Now, "AnyPosition", "AnyCity", 1000);

            // Act
            var userName = employee.CreateUserName();

            // Assert
            Assert.StartsWith(expectedStart, userName);
        }

        [Fact]
        public void GetAge_CalculatesCorrectly()
        {
            // Arrange
            DateTime birthday = new DateTime(2000, 1, 1);
            var employee = new Employee("Test User", birthday, "Tester", "TestCity", 1000);

            // Act
            int age = employee.GetAge();

            // Assert
            int expectedAge = DateTime.Now.Year - 2000;
            if (DateTime.Now < birthday.AddYears(expectedAge))
            {
                expectedAge--;
            }
            Assert.Equal(expectedAge, age);
        }

        [Fact]
        public void ReturnEmployeeInfo_SetsLabelText()
        {
            // Arrange
            var employee = new Employee("Test User", new DateTime(1990, 1, 1), "Tester", "TestCity", 2000);

            // Act
            Label label = employee.ReturnEmployeeInfo();

            // Assert
            Assert.NotNull(label);
            Assert.Contains($"Name: {employee.Name}", label.Text);
            Assert.Contains($"Age: {employee.GetAge()}", label.Text);
            Assert.Contains($"City: {employee.City}", label.Text);
            Assert.Equal("Arial", label.Font.Name);
            Assert.True(label.AutoSize);
        }

        [Fact]
        public void AddTicket(){
            // Arrange
            var employee = new Employee("Test User", new DateTime(1990, 1, 1), "Tester", "TestCity", 2000);
            Assert.Empty(employee.tickets);

            var customer = new Customer("Test User", new DateTime(1995, 12, 4), "Test Address");

            var ticket = new Ticket(customer.ID, 100);

            employee.AddTicket(ticket.ID);
        
        }
    }
}
