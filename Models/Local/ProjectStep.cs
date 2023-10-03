using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class ProjectStep
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? RespDepartment { get; set; } = string.Empty;
        public string? RespEmployee { get; set; } = string.Empty;
        public DateTime? DateSrok { get; set; }
        public DateTime? DateFact { get; set; }
        public string? Description { get; set; } = string.Empty;

    }
}
