using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
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
            modelBuilder.Entity<Dependent>().HasKey(x => new{x.Essn, x.Dependent_name});

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Dependent)
                .WithMany(e => e.)
                .HasForeignKey(x => x.Essn);
        }
    }
}