using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public Department? Department { get; set; }
        public User? User { get; set; }
        public int DepartmentId { get; set; }
        public int? UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string? PhotoUrl { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? LeftDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
