namespace Customers
{
    class CustomerHandler
    {
        // Singleton instance
        private static CustomerHandler _instance;
        private List<Customer> customers;

        // Private constructor to prevent external instantiation
        private CustomerHandler()
        {
            customers = new List<Customer>();
        }

        // Public method to get the singleton instance
        public static CustomerHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CustomerHandler();
                }
                return _instance;
            }
        }

        // Add an customer to the list
        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        // Get all customers
        public List<Customer> GetCustomers()
        {
            return customers;
        }

        // Remove an customer by ID
        public bool RemoveCustomer(int id)
        {
            var customer = customers.Find(customer => customer.ID == id);
            if (customer != null)
            {
                customers.Remove(customer);
                return true;
            }
            return false;
        }

        public Label PrintCustomer(int id)
        {
            var customer = customers.Find(customer => customer.ID == id);

            if (customer == null)
            {
                return null;
            }

            return customer.ReturnCustomerInfo();
        }

        public int getIDByName(string name)
        {
            var customer = customers.Find(customer => customer.ToString().Contains($"Name: {name}"));

            if (customer == null)
            {
                return 0;
            }

            return customer.ID;
        }


        public int GetNextID()
        {
            // No customers yet, return 0
            if (customers.Count == 0)
            {
                return 0;
            }

            int maxID = customers.Max(customer => customer.ID);
            return maxID + 1;
        }
    }
}