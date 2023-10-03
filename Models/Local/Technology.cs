using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class Technology
    {
        [Key]
        public long Id { get; set; }
        public int DseId { get; set; }
        public int? DepRouteId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? TechDate { get; set; }
        public int? TechCompletionPercentage { get; set; }
    }
}
