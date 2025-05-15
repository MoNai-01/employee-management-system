using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmploymentHistory> EmploymentHistories { get; set; }
    }
}