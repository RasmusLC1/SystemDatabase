using Customers;
using Employees;

namespace Tickets
{
    public class Ticket
    {
        public int ID { get; private set; }
        public int TicketID { get; private set; }
        public int EmployeeID { get; private set; }
        public int CustomerID { get; private set; }
        public DateTime Creation { get; private set; }
        public DateTime? Completed { get; private set; }
        public int? Price { get; private set; }
        Label TicketLabel = new Label();
        private static TicketHandler ticketHandler = TicketHandler.Instance;
        private static EmployeeHandler employeeHandler = EmployeeHandler.Instance;
        private static CustomerHandler customerHandler = CustomerHandler.Instance;




        public Ticket(int customerID, int? price = null)
        {
            // var employeeHandler = EmployeeHandler.Instance;
            ID = ticketHandler.GetNextID();
            CustomerID = customerID;
            Creation = DateTime.Now;
            Completed = null;
            Price = price;
            ticketHandler.AddTicket(this);
        }

        // Get current age of ticket
        public int GetAge()
        {
            return DateTime.Now.Day - Creation.Day;
        }

        public bool AddEmployee(int employeeID){
            var employee = employeeHandler.GetEmployee(employeeID);

            if (employee == null){
                return false;
            }
            EmployeeID = employeeID;
            return true;
        }

        public void SetComplete()
        {
            Completed = DateTime.Now;
        }

        public Label ReturnTicketInfo()
        {
            TicketLabel.Text = $"ID: {ID}, Employee: {employeeHandler.GetEmployeeName(EmployeeID)}, Customer: {customerHandler.GetCustomerName(CustomerID)}, Price: {this.Price}";
            TicketLabel.Font = new Font("Arial", 14, FontStyle.Bold); // Set a readable font
            TicketLabel.AutoSize = true; // Ensure the label resizes to fit the text

            TicketLabel.Anchor = AnchorStyles.None;

            return TicketLabel;
        }

    }

}