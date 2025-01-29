using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Xunit;
using Employees;

namespace EmployeeDatabase.Tests
{
    public class EmployeeHandlerTests
    {
        // This constructor runs before each test in xUnit.
        // We use reflection to clear the private 'employees' list 
        // so each test starts with a clean slate.
        public EmployeeHandlerTests()
        {
            var handler = EmployeeHandler.Instance;
            var employeesField = typeof(EmployeeHandler)
                .GetField("employees", BindingFlags.NonPublic | BindingFlags.Instance);
            
            (employeesField?.GetValue(handler) as List<Employee>)?.Clear();
        }

        

        [Fact]
        public void Instance_ReturnsSameSingleton()
        {
            // Arrange & Act
            var instance1 = EmployeeHandler.Instance;
            var instance2 = EmployeeHandler.Instance;

            // Assert
            Assert.Same(instance1, instance2);
        }

        

        [Fact]
        public void AddEmployee_ShouldStoreEmployee()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            var newEmployee = new Employee("John Doe", new DateTime(1990, 1, 1), "Developer", "Seattle", 60000);


            // Assert
            var allEmployees = handler.GetEmployees();
            Assert.Single(allEmployees);
            Assert.Contains(newEmployee, allEmployees);
        }

        [Fact]
        public void Clear_Employees()
        {
            var handler = EmployeeHandler.Instance;
            var newEmployee = new Employee("John Doe", new DateTime(1990, 1, 1), "Developer", "Seattle", 60000);
            handler.AddEmployee(newEmployee);
            int handlerCountStart = handler.GetEmployeeCount();

            handler.ClearEmployees();
            int handlerCountAfterClear = handler.GetEmployeeCount();


            // Assert
            Assert.NotEqual(handlerCountStart, handlerCountAfterClear);
        }

        [Fact]
        public void RemoveEmployee_ShouldReturnTrue_IfEmployeeExists()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            var existingEmployee = new Employee("Jane Doe", new DateTime(1992, 2, 2), "QA", "New York", 55000);

            // Act
            bool removed = handler.RemoveEmployee(existingEmployee.ID);

            // Assert
            Assert.True(removed);
            Assert.Empty(handler.GetEmployees());
        }

        [Fact]
        public void RemoveEmployee_ShouldReturnFalse_IfEmployeeNotFound()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            // No employees added, so ID=999 shouldn't exist.

            // Act
            bool removed = handler.RemoveEmployee(999);

            // Assert
            Assert.False(removed);
        }

        [Fact]
        public void PrintEmployee_ShouldReturnLabel_IfEmployeeExists()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            var employee = new Employee("Bob Smith", new DateTime(1985, 5, 20), "Manager", "Boston", 70000);
            handler.AddEmployee(employee);

            // Act
            Label label = handler.PrintEmployee(employee.ID);

            // Assert
            Assert.NotNull(label);
            Assert.Contains($"Name: {employee.Name}", label.Text);
        }

        [Fact]
        public void PrintEmployee_ShouldReturnNull_IfEmployeeNotFound()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            // No employees added yet.

            // Act
            Label label = handler.PrintEmployee(999);

            // Assert
            Assert.Null(label);
        }

        [Fact]
        public void GetNextID_ReturnsIncrementedValue()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            handler.ClearEmployees();
            var e1 = new Employee("E1", DateTime.Today, "Dev", "CityA", 50000);
            int employee1Count = handler.GetEmployeeCount();
            var e2 = new Employee("E2", DateTime.Today, "Dev", "CityB", 50000);
            int employee2Count = handler.GetEmployeeCount();



            // Next ID should be 3.
            int nextID = handler.GetNextID();

            // Assert
            Assert.Equal(e1.ID, employee1Count);
            Assert.Equal(e2.ID, employee2Count);
            Assert.Equal(employee2Count+1, nextID);
        }

        [Fact]
        public void GetEmployeeName_ReturnsNull_IfNotFound()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;

            // Act
            string name = handler.GetEmployeeName(123); // no employees yet

            // Assert
            Assert.Null(name);
        }

        [Fact]
        public void GetEmployeeName_ReturnsCorrectName_IfFound()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            handler.ClearEmployees();
            var emp = new Employee("Alice Smith", new DateTime(1991, 7, 10), "Support", "Houston", 45000);
            handler.AddEmployee(emp);

            // Act
            string returnedName = handler.GetEmployeeName(emp.ID);

            // Assert
            Assert.Equal("Alice Smith", returnedName);
        }

        [Fact]
        public void getIDByName_ShouldReturnZero_IfNotFound()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            handler.ClearEmployees();
            // No employees added

            // Act
            int id = handler.getIDByName("Nonexistent");

            // Assert
            Assert.Equal(0, id);
        }

        [Fact]
        public void getIDByName_ShouldReturnID_IfMatchFound()
        {
            // Arrange
            var handler = EmployeeHandler.Instance;
            handler.ClearEmployees();
            var emp = new Employee("Charlie Brown", new DateTime(1980, 3, 15), "Cartoonist", "PeanutsTown", 40000);
            handler.AddEmployee(emp);

            // Act
            int foundID = handler.getIDByName("Charlie Brown");

            // Assert
            Assert.Equal(emp.ID, foundID);
        }
    }
}
