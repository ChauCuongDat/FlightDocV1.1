﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightDocV1._1.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        public int Level { get; set; }

        //ForeignKey
        public int? GroupID { get; set; }
        [ForeignKey("GroupID")]
        public Group Group { get; set; }

        public int DocTypeID { get; set; }
        [ForeignKey("DocTypeID")]
        public DocType DocType { get; set; }
    }
}
