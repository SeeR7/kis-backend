using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models._1C_DB
{
    public class VydachaTrebov
    {
        [Key]
        public long Id { get; set; }
        public long DseId { get; set; }
        public DSE? DSE { get; set; }
        public int NumberTreb { get; set; }
        public int Treb { get; set; }
        public DateTime TrebDate { get; set; }
        public int? Vydano { get; set; }
        public DateTime? VydanoDate { get; set; }
        public string? MaterialZam { get; set; }

    }
}
