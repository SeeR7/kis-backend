using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models._1C_DB
{
    public class RNO
    {
        [Key]
        public int Id { get; set; }
        public long DseId { get; set; }
        public DSE? DSE { get; set; }
        public string RegNumber { get; set; }
        public DateTime VpDate { get; set; }
        public DateTime? RegDate { get; set; }
        public string TrebMaterial { get; set; }
        public string SchemeMaterial { get; set; }
        public string? FactMaterial { get; set; }
        public string? DocumentNumber { get; set; }
        public string? DocumentURL { get; set; }
    }
}
