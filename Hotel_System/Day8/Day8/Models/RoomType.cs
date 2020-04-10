using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Day8.Models
{

    public enum Category
    {
      singleRoom,
      doubleRoom,
      tripleRoom,
      familyRoom,
      royalRoom,
    };
    public class RoomType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category type { get; set; }
        
        [ForeignKey("Hotel")]
        public int hotelID { get; set; }
        public Hotel Hotel { get; set; }
        public virtual ICollection<Room> rooms { get; set; }

        public virtual ICollection<Room_Images> Room_Images { get; set; }


    }
}