using System;

namespace EmployeeManagementSystem.Models
{
    public class EmploymentHistory
    {
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Position { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Foreign key for Employee
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}