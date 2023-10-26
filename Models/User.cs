using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FlightDocV1._1.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserSection UserSection { get; set; }
    }
}
