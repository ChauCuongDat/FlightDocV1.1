using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace FlightDocV1._1.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Aircraft { get; set; }
        public string From_Location { get; set; }
        public string To_Location { get; set;}
        public DateTime From_Time { get; set; }
        public DateTime To_Time { get; set;}
        public string Signature { get; set; }

        //Connection
        public ICollection<Document> Documents { get; set; }

    }
}
