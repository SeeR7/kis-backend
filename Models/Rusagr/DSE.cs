using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Rusagr
{
    public class DSE
    {
        [Key]
        public long Id { get; set; }
        public string DseCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string ZagType { get; set; } = string.Empty;
        public string DepCons { get; set; } = string.Empty;
        public string DepProd { get; set; } = string.Empty;
        public string? Material { get; set; } = string.Empty;
    }
}
