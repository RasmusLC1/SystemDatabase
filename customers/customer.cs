using Tickets;

namespace Customers{
    class Customer {
        private static CustomerHandler customerHandler = CustomerHandler.Instance;
        private static TicketHandler ticketHandler= TicketHandler.Instance;
        public int ID {  private set;get; }
        public string Name {get; private set;}
        public DateTime Birthday {get; private set;}
        public string Address {get; private set;}
        private List<int> tickets;
        Label EmployeeLabel = new Label();


    public Customer(string name, DateTime birthday, string address){
        ID = customerHandler.GetNextID();
        Name = name;
        Birthday = birthday;
        Address = address;
        tickets = new List<int>();
    }

    // Get current age of customer
    public int GetAge(){
        return DateTime.Now.Year - Birthday.Year;
    }

    public void AddTicket(int ticketID){
        tickets.Add(ticketID);
        return;
    }

    

    public Label ReturnCustomerInfo()
        {
            EmployeeLabel.Text = $"ID: {ID}, Name: {Name}, Age: {GetAge()}, Address: {Address}, Tickets count: {tickets.Count}";
            EmployeeLabel.Font = new Font("Arial", 14, FontStyle.Bold); // Set a readable font
            EmployeeLabel.AutoSize = true; // Ensure the label resizes to fit the text

            EmployeeLabel.Anchor = AnchorStyles.None;

            return EmployeeLabel;
        }

    }
}