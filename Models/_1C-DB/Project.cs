using System.ComponentModel.DataAnnotations;

namespace ServerApp.Models._1C_DB
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public string ProjectType { get; set; }
        public string ProjectNumber { get; set; }
        public string Consumer { get; set; }
        public DateTime SrokDate { get; set; }
        public string Destination { get; set; }
    }
}
