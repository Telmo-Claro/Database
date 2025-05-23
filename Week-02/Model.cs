using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Week_02;

/*
 * The goal of this week:
 * Make a new DB using the one from week01 but this time with Entity Framework
 */

public class Model
{
    public class MyContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Dept_Location> Dept_Locations { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Works_on> Works_ons { get; set; }
        public DbSet<Dependent> Dependents { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string UserID = "postgres";
            string DBName = "Week02"; //change it accordingly
            string Host = "localhost";//127.0.0.1
            string Port = "5432";
            optionsBuilder.UseNpgsql($"User ID={UserID};Host={Host};Port={Port};Database={DBName};Pooling=true;");
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(x => x.Ssn);
            modelBuilder.Entity<Department>().HasKey(x => x.Dnumber);
            modelBuilder.Entity<Dept_Location>().HasKey(x => new{x.Dnumber, x.Dlocation});
            modelBuilder.Entity<Project>().HasKey(x => x.Pnumber);
            modelBuilder.Entity<Works_on>().HasKey(x => new{x.Essn, x.Pno});
            modelBuilder.Entity<Dependent>().HasKey(x => new{x.Essn, x.Dependen_name});

            modelBuilder.Entity<Employee>()
                .HasOne<Department>()
                .WithMany()
                .HasForeignKey(em => em.Dno);
        }
    }

    public class Employee
    {
        public string Fname { get; set; }
        public string Minit { get; set; }
        public string Lname { get; set; }
        public string Ssn { get; set; }
        public DateTime Bday { get; set; }
        public string Address { get; set; }
        public char Sex { get; set; }
        public int Salary { get; set; }
        public string Super_ssn { get; set; }
        public string Dno { get; set; }
    }

    public class Department
    {
        public string Dname { get; set; }
        public string Dnumber { get; set; }
        public string Mgr_ssn { get; set; }
        public DateTime Mgr_start_date { get; set; }
    }

    public class Dept_Location
    {
        public string Dnumber { get; set; }
        public string Dlocation { get; set; }
    }

    public class Project
    {
        public string Pname { get; set; }
        public string Pnumber { get; set; }
        public string Plocation { get; set; }
        public DateTime? Dnum { get; set; }
    }

    public class Works_on
    {
        public string Essn { get; set; }
        public string Pno { get; set; }
        public int Hours { get; set; }
    }

    public class Dependent
    {
        public string Essn { get; set; }
        public string Dependen_name { get; set; }
        public char Sex { get; set; }
        public DateTime Bday { get; set; }
        public string Relationship { get; set; }
    }
}