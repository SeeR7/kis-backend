using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models.Rusagr
{
    public class DseSostav
    {
        [Key]
        public long Id { get; set; }
        public long ChildId { get; set; }
        public DSE? Child { get; set; }
        public long? ParentId { get; set; }
        public DSE? Parent { get; set;}
        public long? AgregatId { get; set; }

    }
}
