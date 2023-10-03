using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string? Description { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
