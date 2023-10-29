using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class UserSection
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //ForeignKey
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }

        public int? GroupID { get; set; }
        [ForeignKey("GroupID")]
        public Group Group { get; set; }

        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        public Role Role { get; set; }

        //Connection
        public ICollection<DocType> DocTypes { get; set; }
        public Document Document { get; set; }
    }
}
