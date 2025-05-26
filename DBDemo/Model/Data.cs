using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Model
{
  public class EmployeeByBirthDateResult
  {
    public string SSN { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
  }
  
  public class EmployeeWithDepartment
  {
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DepartmentName { get; set; }
  }

  public class EmployeeCountPerDepartment
  {
    public string DepartmentNumber { get; set; }
    public string DepartmentName { get; set; }
    public int EmployeeCount { get; set; }
  }
  public class Data
  {
    public CompanyContext Context { get; set; }

    public Data()
    {
      Context = new CompanyContext();
    }
    public IQueryable<EmployeeByBirthDateResult> GetEmployeesByBirthDate(int year)
    {
      var employeesBeforedate =
        // from employee in Context.Employee
        // where employee.BirthDate < new DateTime(year, 1, 1)
        // select new EmployeeByBirthDateResult { SSN = employee.SSN, FirstName = employee.FirstName, LastName = employee.LastName };
        Context.Employee.Where(employee => employee.BirthDate < new DateTime(year, 1, 1))
        .Select(employee => new EmployeeByBirthDateResult { SSN = employee.SSN, FirstName = employee.FirstName, LastName = employee.LastName });
      Console.WriteLine($"Generated SQL:\n{employeesBeforedate.ToQueryString()}");
      return employeesBeforedate;
    }

    public IQueryable<EmployeeWithDepartment> GetEployeeWithDepartment()
    {
      var result =
        from employee in Context.Employee
        join department in Context.Department on employee.DepartmentNumber equals department.Number
        select new EmployeeWithDepartment 
        { 
          DepartmentName = department.Name, 
          FirstName = employee.FirstName ,
          LastName = employee.LastName
        };
        // Context.Employee.Join(
        //  Context.Department,
        //  employee => employee.DepartmentNumber,
        //  department => department.Number,
        //  (employee, department) =>
        //   new EmployeeWithDepartment 
        //   { 
        //     DepartmentName = department.Name, 
        //     FirstName = employee.FirstName ,
        //     LastName = employee.LastName
        //   }
        // );
      Console.WriteLine($"Generated SQL:\n{result.ToQueryString()}");
      return result;
    }

    public IQueryable<EmployeeWithDepartment> GetEmployeeWithPossibleDepartment()
    {
      var result =
        from employee in Context.Employee
        join department in Context.Department on employee.DepartmentNumber equals department.Number into departmentJoin
        from maybeDepartment in departmentJoin.DefaultIfEmpty()
        select new EmployeeWithDepartment 
        { 
          DepartmentName = maybeDepartment == null ? null : maybeDepartment.Name, 
          FirstName = employee.FirstName ,
          LastName = employee.LastName
        };
        // Context.Employee.Join(
        //  Context.Department,
        //  employee => employee.DepartmentNumber,
        //  department => department.Number,
        //  (employee, department) =>
        //   new EmployeeWithDepartment 
        //   { 
        //     DepartmentName = department.Name, 
        //     FirstName = employee.FirstName ,
        //     LastName = employee.LastName
        //   }
        // );
      Console.WriteLine($"Generated SQL:\n{result.ToQueryString()}");
      return result;
    }

    public IQueryable<EmployeeWithDepartment> GetDepartmentsWithMaybeEmployees()
    {
      var result =
        from department in Context.Department
        join employee in Context.Employee on department.Number equals employee.DepartmentNumber into employeeJoin
        from maybeEmployee in employeeJoin.DefaultIfEmpty()
        select new EmployeeWithDepartment 
        { 
          DepartmentName = department.Name, 
          FirstName = maybeEmployee == null ? null : maybeEmployee.FirstName ,
          LastName = maybeEmployee == null ? null : maybeEmployee.LastName
        };
      Console.WriteLine($"Generated SQL:\n{result.ToQueryString()}");
      return result;
    }

    public IQueryable<EmployeeWithDepartment> GetDepartmentsAndEmployeesFull()
    {
      var result =
        GetDepartmentsWithMaybeEmployees()
        .Union(GetEmployeeWithPossibleDepartment());
      Console.WriteLine($"Generated SQL:\n{result.ToQueryString()}");
      return result;
    }

    public IQueryable<EmployeeCountPerDepartment> GetEmployeeCountPerDepartment()
    {
      var result =
        (from employee in Context.Employee
        join department in Context.Department on employee.DepartmentNumber equals department.Number
        group department by department.Number into departmentGroup
        select new EmployeeCountPerDepartment
        {
          DepartmentNumber = departmentGroup.Key,
          DepartmentName = departmentGroup.First().Name,
          EmployeeCount = departmentGroup.Count()
        })
        .Where(dg => dg.EmployeeCount > 21);
      Console.WriteLine($"Generated SQL:\n{result.ToQueryString()}");
      return result;
    }

    //(a, b, c) x (1, 2) = ((a, 1),  (a, 2) (b, 1), (b, 2), (c, 1), (c, 2))
    public void AddData()
    {
      using (var context = new CompanyContext())
      {
        var employee = new Employee
        {
          SSN = "XYZ347321",
          Address = "Somewhere",
          BirthDate = new DateTime(1985, 9, 9),
          FirstName = "John",
          Gender = Gender.Male,
          LastName = "Doe",
          Salary = 2000,
        };
        var department = new Department
        {
          Name = "Some department",
          Number = "123456",
          ManagerStartDate = DateTime.Now
        };
        var project = new Project
        {
          Name = "Useless stuff",
          Location = "Somewhere else",
          Number = "123456",
        };
        var schedule = new WorksOn
        {
          Hours = 10,
          //this fills in automatically the correct FK values in the db.
          Employee = employee,
          Project = project
        };
        context.WorksOn.Add(schedule);
        context.Employee.Add(employee);
        context.Project.Add(project);
        context.SaveChanges();
      }
    }
  }
}