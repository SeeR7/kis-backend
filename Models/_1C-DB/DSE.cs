using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models._1C_DB
{
    public class DSE
    {
        [Key]
        public long Id { get; set; }
        public string DseCode { get; set; }
        public string Name { get; set; }
        public int PlanZapuska { get; set; }
        public DateTime PlanTrebDate { get; set; }
        public int? PlanSdachi { get; set; }
        public string? StockAvail { get; set; }
        public int? Vydano { get; set; }
        public DateTime? DateVydachi { get; set; }
        public DateTime? FactMechDepDate { get; set; }
        public int? QuantityMechDep { get; set; }
        public DateTime? FactProdDepDate { get; set; }
        public int? QuantityProdDep { get; set; }
        public string? SerialGroup { get; set; }

    }
}
