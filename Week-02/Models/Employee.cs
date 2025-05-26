using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Employee
{
    // Scalar Properties
    public string Fname { get; set; }
    public string Minit { get; set; }
    public string Lname { get; set; }
    public string Ssn { get; set; } // Primary Key
    public DateTime Bday { get; set; }
    public string Address { get; set; }
    public char Sex { get; set; }
    public int Salary { get; set; }
    public string Super_ssn { get; set; }
    public string Dno { get; set; }

    // Navigation Properties
    public Dependent Dependent { get; set; }
    public Department Department { get; set; }

}
