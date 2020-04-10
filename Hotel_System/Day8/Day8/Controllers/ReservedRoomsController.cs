using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Day8.Models;
using Microsoft.AspNet.Identity;
using System.Windows;
//using Microsoft.AspNet.Identity.IdentityExtensions;
namespace Day8.Controllers
{
    [Authorize]
    public class ReservedRoomsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ReservedRooms
        //public async Task<ActionResult> Index()
        //{
        //    var reservedRooms = db.ReservedRooms.Include(r => r.Customer).Include(r => r.Room);
        //    return View(await reservedRooms.ToListAsync());
        //}

        // GET: ReservedRooms/Details/5
        //public async Task<ActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReservedRoom reservedRoom = await db.ReservedRooms.FindAsync(id);
        //    if (reservedRoom == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(reservedRoom);
        //}

        // GET: ReservedRooms/Create
        public ActionResult Create(DateTime sd, DateTime ed,int? RoomID)

        {
            Room room = db.Rooms.Where(r => r.RoomID == RoomID).Select(r => r).First();
            //checkhere
            ViewBag.sd = sd;
            ViewBag.ed = ed;
            ViewBag.RoomID = room.RoomID;
            ViewBag.RoomPrice = room.RoomPrice;
            ViewBag.CustomerID = User.Identity.GetUserId();
            return View();
        }

        // POST: ReservedRooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservedRoomID,CustomerID,RoomID,ReservedRoomStartDate,ReservedRoomEndDate,TotalPrice")] ReservedRoom reservedRoom)
        {
            if (ModelState.IsValid)
            {
                DateTime sd = new DateTime();
                sd=(DateTime)reservedRoom.ReservedRoomStartDate;
                  ((Customer)db.Customers.Select(c => c).Where(c=>c.Id== reservedRoom.CustomerID).First()).budget -= reservedRoom.TotalPrice;
                if (((Customer)db.Customers.Select(c => c).First()).budget > 0) {
                    var rmlist = (db.ReservedRooms.Where(rm => rm.CustomerID == reservedRoom.CustomerID && rm.RoomID == reservedRoom.RoomID && rm.ReservedRoomEndDate == rm.ReservedRoomEndDate && rm.ReservedRoomStartDate == reservedRoom.ReservedRoomStartDate).Select(rm => rm).ToList());
                    
                    if (rmlist.Count==0) 
                    { 
                        db.ReservedRooms.Add(reservedRoom);
                        db.SaveChanges();
                        return Content("SuccssFully Booking");
                    }
                    else
                    {
                        return Content("Try agin not allow to  reseve  this room");
                    }
                }
                else
                {
                    return Content("Sorry Your Budget Is Not Enough");
                }
            }
            return View(reservedRoom);
        }

        // GET: ReservedRooms/Edit/5
        //public async Task<ActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReservedRoom reservedRoom = await db.ReservedRooms.FindAsync(id);
        //    if (reservedRoom == null)
        //    {
        //        return HttpNotFound();
        //    }
        //       return View(reservedRoom);
        //}

        // POST: ReservedRooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(Include = "ReservedRoomID,CustomerID,RoomID,ReservedRoomStartDate,ReservedRoomEndDate,TotalPrice")] ReservedRoom reservedRoom)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(reservedRoom).State = EntityState.Modified;
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.CustomerID = new SelectList(db.Customers, "CustomerID", "Email", reservedRoom.CustomerID);
        //    ViewBag.RoomID = new SelectList(db.Rooms, "RoomID", "RoomDescription", reservedRoom.RoomID);
        //    return View(reservedRoom);
        //}

        // GET: ReservedRooms/Delete/5
        //public async Task<ActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ReservedRoom reservedRoom = await db.ReservedRooms.FindAsync(id);
        //    if (reservedRoom == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(reservedRoom);
        //}

        //// POST: ReservedRooms/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> DeleteConfirmed(int id)
        //{
        //    ReservedRoom reservedRoom = await db.ReservedRooms.FindAsync(id);
        //    db.ReservedRooms.Remove(reservedRoom);
        //    await db.SaveChangesAsync();
        //    return RedirectToAction("Index");
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
