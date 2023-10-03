using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models._1C_DB
{
    public class ProjectAgregat
    {
        [Key]
        public long Id { get; set; }
        public long AgregatId { get; set; }
        public int? ProjectId { get; set; }
        public Project? Project { get; set; }
        public string AgregatName { get; set; }
        public string Description { get; set; }
        public int KolvoUstPart { get; set; }
        public int KolvoIzdIsp { get; set; }
        public int KolvoIzdOtg { get; set; }

    }
}
