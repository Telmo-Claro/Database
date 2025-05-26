using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class Model
{
    public class MyContext : DbContext
    {
        private DbSet<Employee> employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string UserID = "postgres";
            string DBName = "test"; //change it accordingly
            string Host = "localhost";//127.0.0.1
            string Port = "5432";
            optionsBuilder.UseNpgsql($"User ID={UserID};Host={Host};Port={Port};Database={DBName};Pooling=true;");
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Debug);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasKey(x => x.ID);
        }
    }

    public class Employee
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public DateOnly? BirthDate { get; set; }
    }
}