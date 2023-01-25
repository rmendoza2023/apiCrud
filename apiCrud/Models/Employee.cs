namespace apiCrud.Models
{
    public class Employee
    {
        public int idEmployee { get; set; }
        public string employeeName { get; set; }
        public string employeeLastName { get; set; }
        public int department { get; set; }
        public string employeeMail { get; set; }
        public int extension { get; set; }
        public int phoneNumber { get; set; }
        public int collaboratorNumber { get; set; }
        public DateTime dateBirth { get; set; }

        // public bytea employeePhoto { get; set; }
        public int workingHours { get; set; }
    
        public int jobTitle { get; set; }


    }
}
