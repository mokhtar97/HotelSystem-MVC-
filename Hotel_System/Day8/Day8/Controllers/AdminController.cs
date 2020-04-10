using Day8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Day8.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Hotels
        public ActionResult HotelIndex()
        {
            return View(db.Hotels.ToList());
        }

        // GET: Hotels/Details/5
        public ActionResult HotelDetails(int? id)
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

        // GET: Hotels/Create
        public ActionResult HotelCreate()
        {
            return View();
        }

        // POST: Hotels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HotelCreate([Bind(Include = "HotelID,HotelName,HotelDescrotion,HotelCity,HotelAveragePrice,HotelRating,HotelLocation")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Hotels.Add(hotel);
                db.SaveChanges();
                return RedirectToAction("HotelIndex");
            }

            return View(hotel);
        }

        // GET: Hotels/Edit/5
        public ActionResult HotelEdit(int? id)
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
        public ActionResult HotelEdit([Bind(Include = "HotelID,HotelName,HotelDescrotion,HotelCity,HotelAveragePrice,HotelRating,HotelLocation")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("HotelIndex");
            }
            return View(hotel);
        }

        // GET: Hotels/Delete/5
        public ActionResult HotelDelete(int? id)
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
        [HttpPost, ActionName("HotelDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult HotelDeleteConfirmed(int id)
        {
            Hotel hotel = db.Hotels.Find(id);
            db.Hotels.Remove(hotel);
            db.SaveChanges();
            return RedirectToAction("HotelIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public ActionResult RoomIndex()
        {
            var rooms = db.Rooms.Include(r => r.roomType);
            return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult RoomDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult RoomCreate()
        {
            ViewBag.Type_ID = new SelectList(db.RoomTypes, "ID", "Name");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomCreate([Bind(Include = "RoomID,RoomDescription,RoomPrice,Type_ID,RoomIsReserved")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("RoomIndex");
            }

            ViewBag.Type_ID = new SelectList(db.RoomTypes, "ID", "Name", room.Type_ID);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult RoomEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.Type_ID = new SelectList(db.RoomTypes, "ID", "Name", room.Type_ID);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomEdit([Bind(Include = "RoomID,RoomDescription,RoomPrice,Type_ID,RoomIsReserved")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("RoomIndex");
            }
            ViewBag.Type_ID = new SelectList(db.RoomTypes, "ID", "Name", room.Type_ID);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult RoomDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("RoomDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult RoomDeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("RoomIndex");
        }







        public ActionResult Room_Type_Index()
        {
            var roomTypes = db.RoomTypes.Include(r => r.Hotel);
            return View(roomTypes.ToList());
        }

        // GET: RoomTypes/Details/5
        public ActionResult Room_Type_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // GET: RoomTypes/Create
        public ActionResult Room_Type_Create()
        {
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName");
            return View();
        }

        // POST: RoomTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Room_Type_Create([Bind(Include = "ID,Name,Description,type,Instock,Actual_Number,hotelID")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                db.RoomTypes.Add(roomType);
                db.SaveChanges();
                return RedirectToAction("Room_Type_Index");
            }

            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", roomType.hotelID);
            return View(roomType);
        }

        // GET: RoomTypes/Edit/5
        public ActionResult Room_Type_Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", roomType.hotelID);
            return View(roomType);
        }

        // POST: RoomTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Room_Type_Edit([Bind(Include = "ID,Name,Description,type,Instock,Actual_Number,hotelID")] RoomType roomType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomType).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Room_Type_Index");
            }
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", roomType.hotelID);
            return View(roomType);
        }

        // GET: RoomTypes/Delete/5
        public ActionResult Room_Type_Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomType roomType = db.RoomTypes.Find(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // POST: RoomTypes/Delete/5
        [HttpPost, ActionName("Room_Type_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Room_Type_DeleteConfirmed(int id)
        {
            RoomType roomType = db.RoomTypes.Find(id);
            db.RoomTypes.Remove(roomType);
            db.SaveChanges();
            return RedirectToAction("Room_Type_Index");
        }














        public ActionResult facilityIndex()
        {
            var facilities = db.Facilities.Include(f => f.hotel);
            return View(facilities.ToList());
        }

        // GET: Facilities/Details/5
        public ActionResult facilityDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilities facilities = db.Facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // GET: Facilities/Create
        public ActionResult facilityCreate()
        {
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName");
            return View();
        }

        // POST: Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult facilityCreate([Bind(Include = "ID,Facility,hotelID")] Facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.Facilities.Add(facilities);
                db.SaveChanges();
                return RedirectToAction("facilityIndex");
            }

            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", facilities.hotelID);
            return View(facilities);
        }

        // GET: Facilities/Edit/5
        public ActionResult facilityEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilities facilities = db.Facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", facilities.hotelID);
            return View(facilities);
        }

        // POST: Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult facilityEdit([Bind(Include = "ID,Facility,hotelID")] Facilities facilities)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facilities).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("facilityIndex");
            }
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", facilities.hotelID);
            return View(facilities);
        }

        // GET: Facilities/Delete/5
        public ActionResult facilityDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facilities facilities = db.Facilities.Find(id);
            if (facilities == null)
            {
                return HttpNotFound();
            }
            return View(facilities);
        }

        // POST: Facilities/Delete/5
        [HttpPost, ActionName("facilityDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult facilityDeleteConfirmed(int id)
        {
            Facilities facilities = db.Facilities.Find(id);
            db.Facilities.Remove(facilities);
            db.SaveChanges();
            return RedirectToAction("Hotel_Images_Index");
        }







        public ActionResult Hotel_Images_Index()
        {
            var hotel_Images = db.Hotel_Images.Include(h => h.hotel);
            return View(hotel_Images.ToList());
        }

        // GET: Hotel_Images/Details/5
        public ActionResult Hotel_Images_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel_Images hotel_Images = db.Hotel_Images.Find(id);
            if (hotel_Images == null)
            {
                return HttpNotFound();
            }
            return View(hotel_Images);
        }

        // GET: Hotel_Images/Create
        public ActionResult Hotel_Images_Create()
        {
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName");
            return View();
        }

        // POST: Hotel_Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Hotel_Images_Create(Hotel_Images hotel_Images, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                hotel_Images.Image = fileName;
                file.SaveAs(path);
                db.Hotel_Images.Add(hotel_Images);

                db.SaveChanges();
                return RedirectToAction("Hotel_Images_Index");
            }

            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", hotel_Images.hotelID);
            return View(hotel_Images);
        }

        // GET: Hotel_Images/Edit/5
        public ActionResult Hotel_Images_Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel_Images hotel_Images = db.Hotel_Images.Find(id);
            if (hotel_Images == null)
            {
                return HttpNotFound();
            }
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", hotel_Images.hotelID);
            return View(hotel_Images);
        }

        // POST: Hotel_Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Hotel_Images_Edit(Hotel_Images hotel_Images, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                hotel_Images.Image = fileName;
                file.SaveAs(path);
                db.Entry(hotel_Images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Hotel_Images_Index");
            }
            ViewBag.hotelID = new SelectList(db.Hotels, "HotelID", "HotelName", hotel_Images.hotelID);
            return View(hotel_Images);
        }

        // GET: Hotel_Images/Delete/5
        public ActionResult Hotel_Images_Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel_Images hotel_Images = db.Hotel_Images.Find(id);
            if (hotel_Images == null)
            {
                return HttpNotFound();
            }
            return View(hotel_Images);
        }

        // POST: Hotel_Images/Delete/5
        [HttpPost, ActionName("Hotel_Images_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Hotel_Images_DeleteConfirmed(int id)
        {
            Hotel_Images hotel_Images = db.Hotel_Images.Find(id);
            db.Hotel_Images.Remove(hotel_Images);
            db.SaveChanges();
            return RedirectToAction("Hotel_Images_Index");
        }






        public ActionResult Room_Images_Index()
        {
            var room_Images = db.Room_Images.Include(r => r.roomType);
            return View(room_Images.ToList());
        }

        // GET: Room_Images/Details/5
        public ActionResult Room_Images_Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room_Images room_Images = db.Room_Images.Find(id);
            if (room_Images == null)
            {
                return HttpNotFound();
            }
            return View(room_Images);
        }

        public ActionResult Room_Images_Create()
        {
            ViewBag.room_typeID = new SelectList(db.RoomTypes, "ID", "Name");
            return View();
        }

        // POST: Room_Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Room_Images_Create(Room_Images room_Images, HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                room_Images.Name = fileName;
                file.SaveAs(path);
                db.Room_Images.Add(room_Images);
                db.SaveChanges();
                return RedirectToAction("Room_Images_Index");
            }

            ViewBag.room_typeID = new SelectList(db.RoomTypes, "ID", "Name", room_Images.room_typeID);
            return View(room_Images);
        }
        // GET: Room_Images/Edit/5
        public ActionResult Room_Images_Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room_Images room_Images = db.Room_Images.Find(id);
            if (room_Images == null)
            {
                return HttpNotFound();
            }
            ViewBag.room_typeID = new SelectList(db.RoomTypes, "ID", "Name", room_Images.room_typeID);
            return View(room_Images);
        }

        // POST: Room_Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Room_Images_Edit(Room_Images room_Images, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                room_Images.Name = fileName;
                file.SaveAs(path);
                db.Entry(room_Images).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Room_Images_Index");
            }
            ViewBag.room_typeID = new SelectList(db.RoomTypes, "ID", "Name", room_Images.room_typeID);
            return View(room_Images);
        }

        // GET: Room_Images/Delete/5
        public ActionResult Room_Images_Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room_Images room_Images = db.Room_Images.Find(id);
            if (room_Images == null)
            {
                return HttpNotFound();
            }
            return View(room_Images);
        }

        // POST: Room_Images/Delete/5
        [HttpPost, ActionName("Room_Images_Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Room_Images_DeleteConfirmed(int id)
        {
            Room_Images room_Images = db.Room_Images.Find(id);
            db.Room_Images.Remove(room_Images);
            db.SaveChanges();
            return RedirectToAction("Room_Images_Index");
        }


        public ActionResult ReservedIndex()
        {
            var reservedRooms = db.ReservedRooms.Include(r => r.Customer).Include(r => r.Room);
            return View(reservedRooms.ToList());
        }


        // GET: ReservedRooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedRoom reservedRoom = db.ReservedRooms.Find(id);
            if (reservedRoom == null)
            {
                return HttpNotFound();
            }
            return View(reservedRoom);
        }

        // GET: ReservedRooms/Create
        public ActionResult Create()
        {
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "ID");
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "RoomDescription");
            return View();
        }

        // POST: ReservedRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservedRoomID,CustomerID,RoomID,ReservedRoomStartDate,ReservedRoomEndDate")] ReservedRoom reservedRoom)
        {
            if (ModelState.IsValid)
            {
                db.ReservedRooms.Add(reservedRoom);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "ID", reservedRoom.CustomerID);
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "RoomDescription", reservedRoom.RoomID);
            return View(reservedRoom);
        }

        // GET: ReservedRooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedRoom reservedRoom = db.ReservedRooms.Find(id);
            if (reservedRoom == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "ID", reservedRoom.CustomerID);
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "RoomDescription", reservedRoom.RoomID);
            return View(reservedRoom);
        }

        // POST: ReservedRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservedRoomID,CustomerID,RoomID,ReservedRoomStartDate,ReservedRoomEndDate")] ReservedRoom reservedRoom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservedRoom).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerID = new SelectList(db.Customers, "ID", "ID", reservedRoom.CustomerID);
            ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "RoomDescription", reservedRoom.RoomID);
            return View(reservedRoom);
        }

        // GET: ReservedRooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReservedRoom reservedRoom = db.ReservedRooms.Find(id);
            if (reservedRoom == null)
            {
                return HttpNotFound();
            }
            return View(reservedRoom);
        }

        // POST: ReservedRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReservedRoom reservedRoom = db.ReservedRooms.Find(id);
            db.ReservedRooms.Remove(reservedRoom);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}