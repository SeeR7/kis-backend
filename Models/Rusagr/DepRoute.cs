using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Rusagr
{
    public class DepRoute
    {
        [Key]
        public long Id { get; set; }
        public long DseId { get; set; }
        public DSE? Dse { get; set; }
        public string DepCons { get; set; }
        public string DepProd { get; set; }
        public DateTime? EntryDate { get; set; }
        public DateTime? OutDate { get; set;}

    }
}
