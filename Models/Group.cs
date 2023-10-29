using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatorEmail { get; set; }
        public string Note { get; set; }
        //Connection
        public ICollection<UserSection> UserSections { get; set; }
        public ICollection<Permission> Permissions { get; set; }
        public ICollection<DocumentPermission> DocumentPermissions { get; set; }
    }
}