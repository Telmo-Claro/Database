using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;

namespace Model
{
  public enum Gender { Male, Female, Other }
  public class CompanyContext : DbContext
  {
    public DbSet<Employee> Employee { get; set; }
    public DbSet<Dependent> Dependent { get; set; }
    public DbSet<Department> Department { get; set; }
    public DbSet<DepartmentLocation> DepartmentLocation { get; set; }
    public DbSet<Project> Project { get; set; }
    public DbSet<WorksOn> WorksOn { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
      builder.UseNpgsql("User ID=postgres;Password=test1234;Host=localhost;Port=5432;Database=Company;Pooling=true;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Employee>()
      .HasKey(e => e.SSN);

      modelBuilder.Entity<Dependent>()
      .HasKey(d => new { d.EmployeeSSN, d.DependentName });

      modelBuilder.Entity<Department>()
      .HasKey(d => d.Number);

      modelBuilder.Entity<DepartmentLocation>()
      .HasKey(l => new { l.DepartmentNumber, l.Location });

      modelBuilder.Entity<Project>()
      .HasKey(p => p.Number);

      modelBuilder.Entity<WorksOn>()
      .HasKey(wo => new { wo.EmployeeSSN, wo.ProjectNumber });

      modelBuilder.Entity<Dependent>()
      .HasOne(d => d.Employee)
      .WithMany(e => e.Dependents)
      .HasForeignKey(d => d.EmployeeSSN);

      modelBuilder.Entity<Department>()
      .HasOne(d => d.Manager)
      .WithMany(e => e.ManagedDepartments)
      .HasForeignKey(d => d.ManagerSSN);

      modelBuilder.Entity<DepartmentLocation>()
      .HasOne(l => l.Department)
      .WithMany(d => d.Locations)
      .HasForeignKey(l => l.DepartmentNumber);

      modelBuilder.Entity<Project>()
      .HasOne(p => p.Department)
      .WithMany(d => d.Projects)
      .HasForeignKey(p => p.DepartmentNumber);

      modelBuilder.Entity<WorksOn>()
      .HasOne(wo => wo.Employee)
      .WithMany(e => e.Schedule)
      .HasForeignKey(wo => wo.EmployeeSSN);

      modelBuilder.Entity<WorksOn>()
      .HasOne(wo => wo.Project)
      .WithMany(e => e.Developers)
      .HasForeignKey(wo => wo.ProjectNumber);

      modelBuilder.Entity<Employee>()
      .HasOne(e => e.Department)
      .WithMany(d => d.Employees)
      .HasForeignKey(e => e.DepartmentNumber);


    }
  }

  public class Employee
  {
    [Column(TypeName = "varchar(50)")]
    public string FirstName { get; set; }
    [Column(TypeName = "varchar(5)")]
    public string MiddleInitials { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string LastName { get; set; }
    [Column(TypeName = "char(9)")]
    public string SSN { get; set; }
    [Column(TypeName = "date")]
    public DateTime BirthDate { get; set; }
    public string Address { get; set; }
    public Gender Gender { get; set; }
    public double Salary { get; set; }
    [Column(TypeName = "char(6)")]
    public string DepartmentNumber { get; set; }
    public Department Department { get; set; }
    public IEnumerable<Dependent> Dependents { get; set; }
    public IEnumerable<Department> ManagedDepartments { get; set; }
    public IEnumerable<WorksOn> Schedule { get; set; }

  }

  public class Dependent
  {
    [Column(TypeName = "char(9)")]
    public string EmployeeSSN { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string DependentName { get; set; }
    public Gender Gender { get; set; }
    [Column(TypeName = "date")]
    public DateTime BirthDate { get; set; }
    public string Relationship { get; set; }
    public Employee Employee { get; set; }

  }

  public class Department
  {
    [Column(TypeName = "varchar(20)")]
    public string Name { get; set; }
    [Column(TypeName = "char(6)")]
    public string Number { get; set; }
    [Column(TypeName = "char(9)")]
    public string ManagerSSN { get; set; }
    [Column(TypeName = "date")]
    public DateTime ManagerStartDate { get; set; }
    public Employee Manager { get; set; }
    public IEnumerable<Employee> Employees { get; set; }
    public IEnumerable<DepartmentLocation> Locations { get; set; }
    public IEnumerable<Project> Projects { get; set; }
  }

  public class DepartmentLocation
  {
    [Column(TypeName = "char(6)")]
    public string DepartmentNumber { get; set; }
    [Column(TypeName = "varchar(20)")]
    public string Location { get; set; }
    public Department Department { get; set; }
  }

  public class Project
  {
    [Column(TypeName = "varchar(20)")]
    public string Name { get; set; }
    [Column(TypeName = "char(6)")]
    public string Number { get; set; }
    public string Location { get; set; }
    [Column(TypeName = "char(6)")]
    public string DepartmentNumber { get; set; } 
    public Department Department { get; set; }
    public IEnumerable<WorksOn> Developers { get; set; }
  }

  public class WorksOn
  {
    [Column(TypeName = "char(9)")]
    public string EmployeeSSN { get; set; }
    [Column(TypeName = "char(6)")]
    public string ProjectNumber { get; set; }
    public int Hours { get; set; }
    public Employee Employee { get; set; }
    public Project Project { get; set; }

  }


}