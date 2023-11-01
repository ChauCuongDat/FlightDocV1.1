using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class Version
    {
        [Key]
        public int Id { get; set; }
        public float VersionNum { get; set; }
        public string FileAddress { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string UpdaterEmail { get; set; }

        //ForeignKey
        public int DocumentID { get; set; }
        [ForeignKey("DocumentID")]
        public Document Document { get; set; }
    }
}
