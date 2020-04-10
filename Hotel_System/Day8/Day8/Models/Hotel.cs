using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Day8.Models
{
    public class Hotel
    {
        [Key]
        public int HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelDescrotion { get; set; }
        public string HotelCity { get; set; }
        public int HotelAveragePrice { get; set; }
        public int HotelRating { get; set; }
        public string HotelLocation { get; set; }
        public virtual ICollection<RoomType> Room_types { get; set; }
        public virtual ICollection<Facilities> Faciliteis { get; set; }
        public virtual ICollection<Hotel_Images> hotel_Images { get; set; }

      
    }
}