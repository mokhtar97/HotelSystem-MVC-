using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Day8.Models
{
    public class Room_Images
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [ForeignKey("roomType")]
        public int room_typeID { get; set; }
        public RoomType roomType { get; set; }

    }
}