namespace Employees{
class EmployeeHandler
    {
        // Singleton instance
        private static EmployeeHandler _instance;
        private List<Employee> employees;

        // Private constructor to prevent external instantiation
        private EmployeeHandler()
        {
            employees = new List<Employee>();
        }

        // Public method to get the singleton instance
        public static EmployeeHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EmployeeHandler();
                }
                return _instance;
            }
        }

        // Add an employee to the list
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        // Get all employees
        public List<Employee> GetEmployees()
        {
            return employees;
        }

        // Remove an employee by ID
        public bool RemoveEmployee(int id) {
            var employee = employees.Find(employee => employee.ID == id);
            if (employee != null) {
                employees.Remove(employee);
                return true;
            }
            return false;
        }

        public Label PrintEmployee(int id){
            var employee = employees.Find(employee => employee.ID == id);

            if (employee == null){
                return null;
            }

            return employee.ReturnEmployeeInfo();
        }

        public int getIDByName(string name){
            var employee = employees.Find(employee => employee.ToString().Contains($"Name: {name}"));

            if (employee == null){
                return 0;
            }
            
            return employee.ID;
        }

        public string GetEmployeeName(int id) {
            var employee = employees.Find(employee => employee.ID == id);

            if (employee == null){
                return null;
            }

            return employee.Name;
        }


        public int GetNextID(){
            // No employees yet, return 0
            if (employees.Count == 0){
                return 0; 
            }

            int maxID = employees.Max(employee => employee.ID);
            return maxID + 1;
        }
    }
}