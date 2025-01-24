namespace Employees{
    class Employee {
        private static EmployeeHandler employeeHandler = EmployeeHandler.Instance;
        public int ID {  private set;get; }
        public string Name {get; private set;}
        public DateTime Birthday {get; private set;}
        public string Position{get; private set;}
        public string City {get; private set;}
        public int Salary{get; private set;}
        Label EmployeeLabel = new Label();




    public Employee(string name, DateTime birthday, string position, string city, int salary){
        ID = employeeHandler.GetNextID();
        Name = name;
        Birthday = birthday;
        Position = position;
        City = city;
        Salary = salary;
    }

    // Get current age of employee
    public int GetAge(){
        return DateTime.Now.Year - Birthday.Year;
    }

    public Label ReturnEmployeeInfo()
        {
            EmployeeLabel.Text = $"ID: {ID}, Name: {Name}, Age: {GetAge()}, Position: {Position}, City: {City}, Salary: {Salary}";
            EmployeeLabel.Font = new Font("Arial", 14, FontStyle.Bold); // Set a readable font
            EmployeeLabel.AutoSize = true; // Ensure the label resizes to fit the text

            EmployeeLabel.Anchor = AnchorStyles.None;

            return EmployeeLabel;
        }

    }
}