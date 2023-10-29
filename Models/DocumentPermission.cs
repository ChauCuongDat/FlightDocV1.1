using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class DocumentPermission
    {
        [Key]
        public int Id { get; set; }

        //ForeignKey
        public int? GroupID { get; set; }
        [ForeignKey("GroupID")]
        public Group Group { get; set; }

        public int DocumentID { get; set; }
        [ForeignKey("DocumentID")]
        public Document Document { get; set; }
    }
}
