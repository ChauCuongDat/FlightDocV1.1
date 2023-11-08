using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NewestVersionID { get; set; }
        public float NewestVersion {  get; set; }
        public DateTime LastModified { get; set; }

        //Foreignkey
        public int? FlightID { get; set; }
        [ForeignKey("FlightID")]
        public Flight Flight { get; set; }

        public int UserSectionID { get; set; }
        [ForeignKey("UserSectionID")]
        public UserSection UserSection { get; set; }

        public int DocTypeID { get; set; }
        [ForeignKey("DocTypeID")]
        public DocType DocType { get; set; }

        //Connection
        public ICollection<Version> Versions { get; set; }
        public ICollection<DocumentPermission> DocumentPermissions { get; set; }
    }
}
