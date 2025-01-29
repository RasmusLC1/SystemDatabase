using Microsoft.VisualBasic;

namespace Tickets{
public class TicketHandler
    {
        // Singleton instance
        private static TicketHandler? _instance;
        private List<Ticket> tickets;

        // Private constructor to prevent external instantiation
        private TicketHandler()
        {
            tickets = new List<Ticket>();
        }

        // Public method to get the singleton instance
        public static TicketHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TicketHandler();
                }
                return _instance;
            }
        }

        // Add an ticket to the list
        public void AddTicket(Ticket ticket)
        {
            tickets.Add(ticket);
        }

        // Get all tickets
        public List<Ticket> GetTickets()
        {
            return tickets;
        }

        // Remove an ticket by ID
        public bool RemoveTicket(int id) {
            var ticket = tickets.Find(ticket => ticket.ID == id);
            if (ticket != null) {
                tickets.Remove(ticket);
                return true;
            }
            return false;
        }

        public Label? PrintTicket(int id){
            var ticket = tickets.Find(ticket => ticket.ID == id);

            if (ticket == null){
                return null;
            }

            return ticket.ReturnTicketInfo();
        }

        public int? GetTicketCustomer(int id) {
            var ticket = tickets.Find(ticket => ticket.ID == id);
            if (ticket == null){
                return null; 
            }
            return ticket.CustomerID;
        }

        public int? GetTicketAgent(int id) {
            var ticket = tickets.Find(ticket => ticket.ID == id);
            if (ticket == null){
                return null; 
            }
            return ticket.EmployeeID;
        }

        public Ticket? GetTicket(int id) {
            var ticket = tickets.Find(ticket => ticket.ID == id);
            if (ticket == null){
                return null; 
            }
            return ticket;
        }


        public int GetNextID(){
            // No tickets yet, return 0
            if (tickets.Count == 0){
                return 1; 
            }

            int maxID = tickets.Max(ticket => ticket.ID);
            return maxID + 1;
        }
    }
}