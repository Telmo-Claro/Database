using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Dependent
{
    // Scalar properties
    public string Essn { get; set; }
    public string Dependent_name { get; set; }
    public char Sex { get; set; }
    public DateTime Bday { get; set; }
    public string Relationship { get; set; }
    
    // Navigation Properties
    public ICollection<Employee> Employee { get; set; }
}
