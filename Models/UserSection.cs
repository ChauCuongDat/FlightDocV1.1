using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class UserSection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("GroupID")]
        public Group? Group { get; set; }

        public ICollection<DocType>? DocTypes { get; set; }
        public Document Document { get; set; }
    }
}
