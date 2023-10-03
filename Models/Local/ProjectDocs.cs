using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class ProjectDocs
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string DocumentName { get; set; }  = string.Empty;
        public string DocumentURL { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
