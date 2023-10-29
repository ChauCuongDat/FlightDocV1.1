using System.ComponentModel.DataAnnotations;

namespace FlightDocV1._1.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        //Connection
        public UserSection UserSection { get; set; }
    }
}
