using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Department
{
    public string Dname { get; set; }
    public string Dnumber { get; set; }
    public Employee Mgr_ssn { get; set; }
    public DateTime Mgr_start_date { get; set; }

    public ICollection<Employee> EmployeesInDepartment { get; set; }

}
