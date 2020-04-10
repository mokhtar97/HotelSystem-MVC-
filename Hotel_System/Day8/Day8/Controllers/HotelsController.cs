using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using Day8.Models;
using Microsoft.AspNet.Identity;

namespace Day8.Controllers
{
    [AllowAnonymous]
    public class HotelsController : Controller
    {
        static List<Hotel> hotels = new List<Hotel>();
        static List<Hotel> hotelsByRange = new List<Hotel>();
        static List<Hotel> hotelsByRate = new List<Hotel>();
        static List<Hotel> hotelsByFacility = new List<Hotel>();
        static bool rateFlag = false;
        static bool rangeFlag = false;
        static bool facilityFlag = false;
        static DateTime sd;
        static DateTime ed;
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize]
        public ActionResult evalatoin(int id)
        {
            TempData["ID"] = id;
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult evalatoin(int num_stars,int? id)
        {
            
                TempData.Keep();
                int id_hotel = int.Parse(TempData["ID"].ToString()); ;
                
            string idd = User.Identity.GetUserId();
                  if(db.ReservedRooms.Select(r => r).Where(r => r.CustomerID == idd && r.Room.roomType.hotelID == id_hotel && r.Customer_Evalution == 0).Select(re => re).ToList().Count > 0)
                {
                    ((ReservedRoom)db.ReservedRooms.Select(r => r).Where(r => r.CustomerID == idd && r.Room.roomType.hotelID == id_hotel && r.Customer_Evalution == 0).First()).Customer_Evalution = num_stars;

                    db.SaveChanges();
                    int i = 0;
                    int sum = 0;
                    foreach (var item in (db.ReservedRooms.Select(r => r).Where(r => r.CustomerID == idd && r.Room.roomType.hotelID == id_hotel)).ToList())
                    {
                        sum += item.Customer_Evalution;
                        i++;
                    }
                 ((Hotel)db.Hotels.Select(h => h).Where(h => h.HotelID == id_hotel).First()).HotelRating = sum / i;
                    db.SaveChanges();
                    return View();
                }
                else
                {
                    return RedirectToAction("index", "hotels"); 
                }
            
        }
        public ActionResult search(string name, string check_in, string check_out, int? room_type)

        {
            ViewBag.id = User.Identity.GetUserId();
            ViewBag.rm = db.ReservedRooms.Select(r => r).ToList();
            hotels = db.Hotels.Where(h => h.HotelCity.Contains(name)).ToList();
            List<Hotel> res = new List<Hotel>();
            sd = new DateTime();
            ed = new DateTime();
             DateTime.TryParse(check_in,out sd);
             DateTime.TryParse(check_out,out ed);

            foreach (var item in hotels)
            {
                foreach (var room_tye_item in item.Room_types)
                {
                    if (room_tye_item.type == (Category)room_type)
                    {
                        var rooms = db.Rooms.Where(r => r.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                        List<Room> roms = new List<Room>();
                        roms.AddRange(rooms);
                        var reserved_rooms = db.ReservedRooms.Where(r => ((r.ReservedRoomEndDate >=sd && r.ReservedRoomStartDate <= sd) || (r.ReservedRoomEndDate >=ed && r.ReservedRoomStartDate <=ed)) && r.Room.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                        //var available_reserved = db.ReservedRooms.Where(r => (((r.ReservedRoomStartDate < sd && r.ReservedRoomEndDate < sd && ed > r.ReservedRoomEndDate && ed > r.ReservedRoomStartDate)) || (r.ReservedRoomStartDate > sd && r.ReservedRoomEndDate > sd && r.ReservedRoomStartDate > ed && r.ReservedRoomEndDate > ed)) && r.Room.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                        //var reserved_rooms = db.ReservedRooms.Where(r => (r.ReservedRoomEndDate > sd || r.ReservedRoomStartDate < ed) && r.Room.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                        //var available_reserved = db.ReservedRooms.Where(r => ((r.ReservedRoomStartDate < sd) && (r.ReservedRoomEndDate < sd)) || ((sd < r.ReservedRoomStartDate) && (sd < r.ReservedRoomStartDate)) && r.Room.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                        foreach (var reserved in reserved_rooms)
                        {
                        foreach (var room in rooms)
                        {
                            if (reserved.Room.RoomID == room.RoomID)
                            {
                                    roms.Remove(room);
                            }
                          }

                        }

                        //foreach (var reserved in available_reserved)
                        //{
                        //    foreach (var room in rooms)
                        //    {
                        //        if (reserved.Room.RoomID == room.RoomID)
                        //        {
                        //            //roms.Add(room);
                                   
                        //        }
                        //    }
                        //}
                    if (roms.Count >0)
                        {
                            res.Add(item);
                            break;
                        }
                    }
                }
            }
            if (res != null)
                return PartialView("_hotels", res);
            else
                return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult FilterByRange(int? range)
        {
            ViewBag.id = User.Identity.GetUserId();
            ViewBag.rm = db.ReservedRooms.Select(r => r).ToList();
            rangeFlag = true;
            if (rateFlag)
                hotels = hotelsByRate;
            if (facilityFlag)
                hotels = hotelsByFacility;
            hotelsByRange.Clear();
            hotelsByRange = hotels.Where(h => h.HotelAveragePrice <= range).ToList();
            if (hotels != null)
                return PartialView("_hotels", hotelsByRange);
            else
                return RedirectToAction("Index");
        }
        public ActionResult FilterByRate(int? rate)
        {
            ViewBag.id = User.Identity.GetUserId();
            ViewBag.rm = db.ReservedRooms.Select(r => r).ToList();

            rateFlag = true;
            if (rangeFlag)
                hotels = hotelsByRange;
            if (facilityFlag)
                hotels = hotelsByFacility;
            hotelsByRate.Clear();
            hotelsByRate = hotels.Where(h => h.HotelRating == rate).ToList();
            if (hotels != null)
                return PartialView("_hotels", hotelsByRate);
            else
                return RedirectToAction("Index");

        }
        public ActionResult FilterByFacility(List<string> checkboxList)
        {
            
            ViewBag.id = User.Identity.GetUserId();
            ViewBag.rm = db.ReservedRooms.Select(r => r).ToList();
            facilityFlag = true;
            if (rateFlag)
                hotels = hotelsByRate;
            if (rangeFlag)
                hotels = hotelsByRange;
            hotelsByFacility.Clear();
            foreach (var hotel in hotels)
            {
                foreach (var facility in hotel.Faciliteis)
                {
                    foreach (var checkbox in checkboxList)
                    {
                        if (facility.Facility == checkbox)
                        {
                            if (!hotelsByFacility.Contains(hotel))
                                hotelsByFacility.Add(hotel);
                        }
                    }
                }
            }
            if (hotelsByFacility != null)
                return PartialView("_hotels", hotelsByFacility);
            else
                return RedirectToAction("Index");
        }

        // GET: Hotels
        public ActionResult Index()
        {
            ViewBag.id =User.Identity.GetUserId();
            ViewBag.rm = db.ReservedRooms.Select(r => r).ToList();
            return View(db.Hotels.ToList());
        }

        // GET: Hotels/Details/5
        List<Room> rooms0;
        List<Room> rooms1;
        List<Room> rooms2;
        List<int> rooms0ID;
        List<int> rooms1ID;
        List<int> rooms2ID;
        public ActionResult Details(int? id)
        {
            var  total0= 0;
            var  total1= 0;
            var  total2= 0;
            rooms0 = new List<Room>();
            rooms1 = new List<Room>();
            rooms2 = new List<Room>();
            rooms0ID = new List<int>();
            rooms1ID=new List<int>();
            rooms2ID =new List<int>();
            var  hotel = db.Hotels.Where(h => h.HotelID==id).First();
            foreach (var room_tye_item in hotel.Room_types)
            {
                if (room_tye_item.type == (Category)0)
                {
                    var rooms = db.Rooms.Where(r => r.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                    rooms0.AddRange(rooms);
                    int count_rooms = rooms.Count();
                    var reserved_rooms = db.ReservedRooms.Where(r => ((r.ReservedRoomEndDate >= sd && r.ReservedRoomStartDate <= sd) || (r.ReservedRoomEndDate >= ed && r.ReservedRoomStartDate <= ed)) && r.Room.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                  
                    foreach (var reserved in reserved_rooms)
                    {
                        foreach (var room in rooms)
                        {
                            if (reserved.Room.RoomID == room.RoomID)
                            {
                                if (rooms0.Contains(room))
                                {
                                    count_rooms--;
                                    rooms0.Remove(room);
                                }
              
                            }
                        }

                    }

                    total0 = count_rooms;
                }
                else if(room_tye_item.type == (Category)1)
                {
                    var rooms = db.Rooms.Where(r => r.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                    rooms1.AddRange(rooms);
                    int count_rooms = rooms.Count();
                    var reserved_rooms = db.ReservedRooms.Where(r => ((r.ReservedRoomEndDate >= sd && r.ReservedRoomStartDate <= sd) || (r.ReservedRoomEndDate >= ed && r.ReservedRoomStartDate <= ed)) && r.Room.roomType.ID == room_tye_item.ID).Select(r => r).ToList();

                    foreach (var reserved in reserved_rooms)
                    {
                        foreach (var room in rooms)
                        {
                            if (reserved.Room.RoomID == room.RoomID)
                            {
                                if (rooms1.Contains(room))
                                {
                                    rooms1.Remove(room);
                                    count_rooms--;
                                }
                            }
                        }
                    }
                    total1 = count_rooms;
                }
                else if (room_tye_item.type == (Category)2)
                {
                    var rooms = db.Rooms.Where(r => r.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                    rooms2.AddRange(rooms);
                    int count_rooms = rooms.Count();
                    var reserved_rooms = db.ReservedRooms.Where(r => ((r.ReservedRoomEndDate >= sd && r.ReservedRoomStartDate <= sd) || (r.ReservedRoomEndDate >= ed && r.ReservedRoomStartDate <= ed)) && r.Room.roomType.ID == room_tye_item.ID).Select(r => r).ToList();
                    foreach (var reserved in reserved_rooms)
                    {
                        foreach (var room in rooms)
                        {
                            if (reserved.Room.RoomID == room.RoomID)
                            {
                                if (rooms2.Contains(room))
                                {
                                    rooms2.Remove(room);
                                    count_rooms--;
                                }
                            }
                        }

                    }

                
                    total2 = count_rooms;
                }
            }
            ViewBag.sd = sd;
            ViewBag.ed = ed;
          
          
            if ((Room)rooms0.FirstOrDefault() != null)
                ViewBag.RoomID0 = ((Room)rooms0.FirstOrDefault()).RoomID;
            //TempData["rooms0"] = rooms0;
            if ((Room)rooms1.FirstOrDefault() != null)
                ViewBag.RoomID1 = ((Room)rooms1.FirstOrDefault()).RoomID;
            //TempData["rooms1"] = rooms1;
            if ((Room)rooms2.FirstOrDefault()!=null)
            ViewBag.RoomID2 = ((Room)rooms2.FirstOrDefault()).RoomID;
            //TempData["rooms2"] = rooms2;
            ViewBag.room_type0 = total0;
            ViewBag.room_type1 = total1;
            ViewBag.room_type2 = total2;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // GET: Hotels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HotelID,HotelName,HotelDescrotion,HotelCity,HotelAveragePrice,HotelRating,HotelLocation")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HotelID,HotelName,HotelDescrotion,HotelCity,HotelAveragePrice,HotelRating,HotelLocation")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }
            return View(hotel);
        }

        // POST: Hotels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult about( )
        {
            
            return View("about");
        }
        public ActionResult Contact()
        {

            return View("Contact");
        }
        //public ActionResult checkStartDate(DateTime ReservedRoomEndDate, DateTime ReservedRoomStartDate)
        //{
        //    if (ReservedRoomEndDate >ReservedRoomStartDate)
        //    {
        //        //valid
        //        return Json(true, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //        //notvalid
        //        return Json(false, JsonRequestBehavior.AllowGet);

        //}
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
