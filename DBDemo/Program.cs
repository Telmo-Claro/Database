using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace DBDemo
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var seeder = new Seeder();
      var data = new Data();
      // seeder.Seed();
      // var employeesWithDepartment = data.GetDepartmentsAndEmployeesFull();
      // foreach (var employeeDepartment in employeesWithDepartment)
      // {
      //   Console.WriteLine($"FirstName = {(employeeDepartment.FirstName == null ? "<null>" : employeeDepartment.FirstName)}, LastName = {(employeeDepartment.LastName == null ? "<null>" : employeeDepartment.LastName)}, DepartmentName = {(employeeDepartment.DepartmentName == null ? "<null>" : employeeDepartment.DepartmentName)}");
      // }
      var employeesPerDeparmtent = data.GetEmployeeCountPerDepartment();
      foreach (var group in employeesPerDeparmtent)
      {

        Console.WriteLine($"Department Number = {group.DepartmentNumber}, Department Name = {group.DepartmentName}, Employee Count = {group.EmployeeCount}");
      }
    }
  }
}