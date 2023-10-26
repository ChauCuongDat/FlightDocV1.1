using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("FlightID")]
        public ICollection<Flight>? Flights { get; set; }
        [ForeignKey("UserSectionID")]
        public ICollection<UserSection> UserSections { get; set; }
        [ForeignKey("DocTypeID")]
        public DocType DocType { get; set; }
        public ICollection<Version> Versions { get; set; }
    }
}
