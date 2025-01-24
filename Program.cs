using System;
using System.Windows.Forms;
using Customers;
using Employees;
using Tickets;

namespace HelloWorldApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the application with the HelloForm
            Application.Run(new HelloForm());
        }
    }

    public class HelloForm : Form
    {
        public HelloForm()
        {
            // Set form properties
            this.Text = "Employee Info";
            this.Size = new System.Drawing.Size(800, 600);

            // Initialize EmployeeHandler singleton
            var employeeHandler = EmployeeHandler.Instance;
            var ticketHandler = TicketHandler.Instance;
            var customerHandler = CustomerHandler.Instance;

            // Add an employee to the handler
            employeeHandler.AddEmployee(new Employee("John Jones", new DateTime(1989, 3, 12), "Dev", "Boston", 1300));
            employeeHandler.AddEmployee(new Employee("Adam", new DateTime(1997, 2, 22), "Assistant", "New York", 1200));
            employeeHandler.AddEmployee(new Employee("Sigrid", new DateTime(1985, 7, 4), "Manager", "Boston", 1600));

            customerHandler.AddCustomer(new Customer("Benny", new DateTime(1956, 12, 5), "Funny street 23"));

            

            // Display the employee information
            AddEmployeeLabels(employeeHandler);
            
            // Handle form resize to dynamically adjust label positions
            this.Resize += (sender, e) => CenterEmployeeLabels();
        }

        private void AddEmployeeLabels(EmployeeHandler handler)
        {
            // Clear existing controls (if any)
            this.Controls.Clear();

            // Dynamically add labels for all employees
            int y = 10; // Initial Y position for the first label
            foreach (var employee in handler.GetEmployees())
            {
                Label label = employee.ReturnEmployeeInfo();
                label.Location = new System.Drawing.Point((this.ClientSize.Width - label.Width) / 2, y);
                this.Controls.Add(label);
                y += label.Height + 10; // Adjust spacing for the next label
            }
        }

        private void CenterEmployeeLabels()
        {
            // Dynamically center labels on form resize
            foreach (Control control in this.Controls)
            {
                if (control is Label label)
                {
                    label.Location = new System.Drawing.Point(
                        (this.ClientSize.Width - label.Width) / 2,
                        label.Location.Y
                    );
                }
            }
        }
    }
}
