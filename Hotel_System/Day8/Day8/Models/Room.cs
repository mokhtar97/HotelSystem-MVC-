using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Day8.Models
{

    public class Room
    {
        [Key]
        public int RoomID { get; set; }
        public string RoomDescription { get; set; }
        public double RoomPrice { get; set; }

        [ForeignKey("roomType")]
        public int Type_ID { get; set; }
        public RoomType roomType { get; set; }
       

    }
}