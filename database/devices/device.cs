using Employees;
using Tickets;

namespace DeviceNameSpace
{
    public class Device
    {
        public static int ID { get; private set; }
        public static DeviceHandler deviceHandler = DeviceHandler.GetInstance(); // Use GetInstance() instead of accessing _instance directly
        public static EmployeeHandler employeeHandler = EmployeeHandler.Instance;
        public static TicketHandler ticketHandler = TicketHandler.Instance;

        public string Location { get; private set; }
        public string Type { get; private set; }  // Removed static
        public int PurchasePrice { get; private set; }  // Removed static
        public int SellPrice { get; private set; }
        Label DeviceLabel;

        public Device(string type, string location, int purchasePrice, int sellPrice)
        {
            this.Location = location;
            this.SellPrice = sellPrice;
            this.Type = type;
            this.PurchasePrice = purchasePrice;
        }

        public int CreatePrice()
        {
            SellPrice = PurchasePrice * 2;
            return SellPrice;
        }
    }
}
