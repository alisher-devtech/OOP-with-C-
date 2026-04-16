namespace FarmingManagementSystem.Models
{
    public class Employee
    {
        private int id;
        private string name;
        private string role;
        private double salary;
        private string joinDate;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public double Salary
        {
            get { return salary; }
            set { salary = value; }
        }

        public string JoinDate
        {
            get { return joinDate; }
            set { joinDate = value; }
        }

        public Employee()
        {
            id = 0;
            name = "";
            role = "";
            salary = 0.0;
            joinDate = "";
        }

        public Employee(int id, string name, string role, double salary, string joinDate)
        {
            this.id = id;
            this.name = name;
            this.role = role;
            this.salary = salary;
            this.joinDate = joinDate;
        }
    }
}