using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Model
{
 
    public class MyContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseNpgsql(@"Host=localhost:5432;Username=postgres;Database=efdb;Maximum Pool Size=200");
            builder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug);
        }
        
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<WorksOn> WorksOn { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>().HasKey(x => x.Number);
            modelBuilder.Entity<WorksOn>().HasKey(x => new { x.ProjectNumber, x.EmployeeID });
            modelBuilder.Entity<WorksOn>()
                .HasOne<Project>()
                .WithMany()
                .HasForeignKey(p => p.ProjectNumber);
            modelBuilder.Entity<WorksOn>()
                .HasOne<Employee>()
                .WithMany()
                .HasForeignKey(e => e.EmployeeID);
        }
    }

    public class Project
    {
        public int Number { get; set; }
        public string? OtherDetails { get; set; }
    }

    public class WorksOn
    {
        public int ProjectNumber { get; set; }
        public Guid EmployeeID { get; set; }
        public int Hours { get; set; }
    }

    public class Employee
    {
        public Guid ID { get; set; }
        
        [Required,Column(TypeName = "varchar(50)")]
        public string Name { get; set; }
        public DateOnly? BirthDate { get; set; }
        
        public Department Department { get; set; } = null!;
        public string DepartmentName;
        
        public Employee? Supervisor { get; set; }
        public Guid? SupervisorID { get; set; }
    }

    public class Department
    {
        [Key]
        public string Name { get; set; } = null!;
        public Employee? Manager { get; set; }
        public Guid? ManagerID { get; set; }
    }
    
    
}