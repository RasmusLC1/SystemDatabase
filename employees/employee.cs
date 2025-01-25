namespace Employees{
    class Employee {
        private static EmployeeHandler employeeHandler = EmployeeHandler.Instance;
        public int ID {  private set;get; }
        public string Name {get; private set;}
        public string Username {get; private set;}
        public DateTime Birthday {get; private set;}
        public string Position{get; private set;}
        public string City {get; private set;}
        public int Salary{get; private set;}
        Label EmployeeLabel = new Label();

    public Employee(string name, DateTime birthday, string position, string city, int salary){
        ID = employeeHandler.GetNextID();
        Name = name.Trim();  // Trim extra 
        Birthday = birthday;
        Position = position;
        City = city;
        Salary = salary;
        Username = CreateUserName();
    }

    // Get current age of employee
    public int GetAge() {
        int age = DateTime.Now.Year - Birthday.Year;
        if (DateTime.Now < Birthday.AddYears(age)) {
            age--; // subtract one if birthday hasn't occurred yet this year
        }
        return age;
    }

    public string CreateUserName(){

        string[] nameParts = this.Name.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        // Split the name in two
        string firstName = nameParts.Length > 0 ? nameParts[0] : string.Empty;
        string lastName = nameParts.Length > 1 ? nameParts[1] : string.Empty;

        // Get the first two letters of each
        string firstTwoOfFirstName = firstName.Length >= 2 ? firstName.Substring(0, 2) : firstName;
        string firstTwoOfLastName = lastName.Length >= 2 ? lastName.Substring(0, 2) : lastName;
        string username = firstTwoOfFirstName+firstTwoOfLastName;
        return username;
    }

    public Label ReturnEmployeeInfo()
        {
            EmployeeLabel.Text = $"ID: {ID}, Username: {Username} Name: {Name}, Age: {GetAge()}, Position: {Position}, City: {City}, Salary: {Salary}";
            EmployeeLabel.Font = new Font("Arial", 14, FontStyle.Bold); // Set a readable font
            EmployeeLabel.AutoSize = true; // Ensure the label resizes to fit the text

            EmployeeLabel.Anchor = AnchorStyles.None;

            return EmployeeLabel;
        }

    }
}