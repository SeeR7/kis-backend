using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public string? Type { get; set; }
    }
}
