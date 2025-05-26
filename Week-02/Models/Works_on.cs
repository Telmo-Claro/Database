using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Works_on
{
    public string Essn { get; set; }
    public string Pno { get; set; }
    public int Hours { get; set; }

    // Navigation properties
    public IEnumerable<Employee> Employees { get; set; }
}