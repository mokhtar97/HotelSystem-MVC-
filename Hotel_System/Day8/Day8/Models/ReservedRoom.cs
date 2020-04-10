using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Windows.Input;

namespace Day8.Models
{
    public  class ReservedRoom
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReservedRoomID { get; set; }
        [ForeignKey("Customer")]
        public string CustomerID { get; set; }
        [ForeignKey("Room")]
        public int RoomID { get; set; }
        [DataType("Date")]
        public DateTime ReservedRoomStartDate { get; set; }
        [DataType("Date")]
        public DateTime ReservedRoomEndDate { get; set; }
        public int TotalPrice { get; set; }
        public int Customer_Evalution { get; set; }
        public virtual Room Room{ get; set; }
        public virtual Customer Customer{ get; set; }
    }
}