using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Day8.Models
{
    public class Hotel_Images
    {
        public int ID { get; set; }
        public string Image { get; set; }
        [ForeignKey("hotel")]
        public int hotelID { get; set; }
        public Hotel hotel { get; set; }
    }
}