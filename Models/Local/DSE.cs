using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Local
{
    public class DSE
    {
        [Key]
        public long Id { get; set; }
        public long DseId { get; set; }
        public DateTime? PlanMechDepData { get; set; }
        public int? QuantityMechDep { get; set; }
        public int? MechDepСompletionPercentage { get; set; }
        public DateTime? PlanProdDepData { get; set; }
        public int? QuantityProdDep { get; set; }
        public int? MechProdСompletionPercentage { get; set; }
        public string? RnoStatus { get; set; }
        public string? Desctiption { get; set; }


    }
}
