using Microsoft.EntityFrameworkCore;
using ReverseAutoMapperDemo.Models;

namespace ReverseAutoMapperDemo.Data
{
    public class EmployeeDBContext : DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    FirstName = "Anurag",
                    LastName = "Mohanty",
                    Email = "anurag.mohanty@example.com",
                    Gender = "Male"
                },
                new Employee
                {
                    Id = 2,
                    FirstName = "Pranaya",
                    LastName = "Rout",
                    Email = "pranaya.rout@example.com",
                    Gender = "Male"
                }
            );

            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    Id = 1,
                    City = "Bhubaneswar",
                    State = "Odisha",
                    Country = "India",
                    EmployeeId = 1
                },
                new Address
                {
                    Id = 2,
                    City = "Mumbai",
                    State = "Maharashtra",
                    Country = "India",
                    EmployeeId = 2
                }
                );
        }

        public DbSet<Employee> Employees {  get; set; }

        public DbSet<Address> Addresses { get; set; }

    }
}
