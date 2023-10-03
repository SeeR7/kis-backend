using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class RNO
    {
        [Key]
        public int Id { get; set; }
        public long DseId { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public bool? DevConsultNeed { get; set; }
        public DateTime? DevDate { get; set; }
        public DateTime? RegDate { get; set; }
        public string? RnoStatus { get; set; }
    }
}
