using Day8.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Windows;

namespace Day8.MyHab
{
    [HubName("BookingHub")]
    public class BookingHub:Hub

    {
        private ApplicationDbContext db;
        [HubMethodName("BookingRoom")]
        public void BookingRoom(int RoomID, DateTime StartDate, DateTime EndDate)
        {
            db = new ApplicationDbContext();
            //var rmlist = (db.ReservedRooms.Where(re => re.RoomID == RoomID && re.ReservedRoomStartDate == StartDate && re.ReservedRoomEndDate == EndDate).Select(rm => rm).ToList());
            ////if (rmlist.Count==1)
            //{
                int ID =db.Rooms.Where(r => r.RoomID == RoomID).Select(r=>r.roomType.ID).First();
                int HotelID = db.RoomTypes.Where(rt => rt.ID == ID).Select(rt => rt.hotelID).First();
                Category category = db.RoomTypes.Where(rt => rt.ID == ID).Select(rt => rt.type).First();
                Clients.All.QuantityDecrease(HotelID, category, StartDate, EndDate);
            //}
            
                
        }

    }
}