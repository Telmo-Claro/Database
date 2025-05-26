using System;

namespace Model
{
  public class Seeder
  {

    private string[] Names =
    {
      "John",
      "Jane",
      "Chris",
      "Christine",
      "Walter",
      "Val",
      "Patricia",
      "Jennifer"
    };

    private string[] LastNames = new string[]
    {
      "White",
      "Kilmer",
      "Cruise",
      "Greene",
      "Jackson",
      "Strickland",
      "Steiner",
      "Robinson",
      "Anderson",
      "Trent"
    };

    private char[] Alphanumeric;
    private char[] Alphabet;

    public Seeder()
    {
      Alphanumeric = new char[36];
      Alphabet = new char[26];
      int j = 0;
      for (int i = 65; i <= 90; i++)
      {
        Alphabet[j] = (char)i;
        Alphanumeric[j] = (char)i;
        j++;
      }
      for (int i = 48; i <= 57; i++)
      {
        Alphanumeric[j++] = (char)i;
      }
    }

    public void Clear()
    {
      using (var context = new CompanyContext())
      {
        Console.WriteLine("Clearing database...");
        context.Employee.RemoveRange(context.Employee);
        context.Dependent.RemoveRange(context.Dependent);
        context.Department.RemoveRange(context.Department);
        context.DepartmentLocation.RemoveRange(context.DepartmentLocation);
        context.Project.RemoveRange(context.Project);
        context.WorksOn.RemoveRange(context.WorksOn);
        context.SaveChanges();
        Console.WriteLine("Done!");
      }
    }

    public void Seed()
    {
      using (var context = new CompanyContext())
      {
        var random = new Random();
        const int employeeCount = 100;
        const int departmentCount = 3;
        Clear();

        Console.WriteLine("Seeding Employees...");
        var employees = new Employee[employeeCount];
        for (int i = 0; i < employeeCount; i++)
        {
          char[] ssnChars = new char[9];
          string initials = Alphabet[random.Next(0, Alphabet.Length)].ToString();
          for (int j = 0; j < ssnChars.Length; j++)
          {
            ssnChars[j] = Alphanumeric[random.Next(0, Alphanumeric.Length)];
          }
          employees[i] = new Employee
          {
            SSN = new String(ssnChars),
            Address = Guid.NewGuid().ToString(),
            BirthDate = DateTime.Now - TimeSpan.FromDays(random.Next(18 * 365, 60 * 365)),
            FirstName = Names[random.Next(0, Names.Length)],
            Gender = (Gender)random.Next(0, 3),
            LastName = LastNames[random.Next(0, LastNames.Length)],
            Salary = 1500 + random.NextDouble() * 3000,
            MiddleInitials = random.NextDouble() < 0.5 ? initials : null 
          };
        }
        context.Employee.AddRange(employees);
        Console.WriteLine("Done!");

        Console.WriteLine("Seeding Departments...");
        Department[] departments = new Department[departmentCount];
        for (int i = 0; i < departmentCount; i++)
        {
          var department = new Department
          {
            Name = random.Next(0, 1000).ToString(),
            Number = random.Next(0, 1000).ToString(),
          };
          departments[i] = department;
        }

        context.Department.AddRange(departments);
        Console.WriteLine("Done!");
        
        Console.WriteLine("Connecting departments to employees...");
        foreach (var employee in employees)
        {
          if (random.NextDouble() > 0.25)
          {
            var randomDepartment = departments[random.Next(0, departmentCount)];
            employee.Department = randomDepartment;
          }
        }
        Console.WriteLine("Done!");
        
        
        context.SaveChanges();
      }
      

    }
  }
}