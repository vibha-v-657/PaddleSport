using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hackathon_Internship.Models;

namespace Hackathon_Internship.Controllers
{
    public class ReservationsController : Controller
    {
        private defaultConnectionEntities db = new defaultConnectionEntities();

        // GET: Reservations
        [Authorize(Roles = "Staff")]

        public ActionResult Index()
        {
            var reservations = db.Reservations.Include(r => r.availability).Include(r => r.court);
            return View(reservations.ToList());
        }

        // GET: Reservations/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.field_id = new SelectList(db.availabilities, "field_id", "field_id");
            ViewBag.venue_id = new SelectList(db.courts, "court_id", "court_name");
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "res_id,field_id,venue_id,cust_id,total_time,total_amt")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.field_id = new SelectList(db.availabilities, "field_id", "field_id", reservation.field_id);
            ViewBag.venue_id = new SelectList(db.courts, "court_id", "court_name", reservation.venue_id);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.field_id = new SelectList(db.availabilities, "field_id", "field_id", reservation.field_id);
            ViewBag.venue_id = new SelectList(db.courts, "court_id", "court_name", reservation.venue_id);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "res_id,field_id,venue_id,cust_id,total_time,total_amt")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.field_id = new SelectList(db.availabilities, "field_id", "field_id", reservation.field_id);
            ViewBag.venue_id = new SelectList(db.courts, "court_id", "court_name", reservation.venue_id);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        [Authorize(Roles = "Staff")]

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservations.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation reservation = db.Reservations.Find(id);
            db.Reservations.Remove(reservation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
