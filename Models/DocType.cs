using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class DocType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }
        [ForeignKey("UserSectionID")]
        public UserSection UserSection { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public Document Document { get; set; }
    }
}
