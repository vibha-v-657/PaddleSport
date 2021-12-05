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
    public class availabilitiesController : Controller
    {
        private defaultConnectionEntities db = new defaultConnectionEntities();

        // GET: availabilities
        public ActionResult Index()
        {
            var availabilities = db.availabilities.Include(a => a.avl_field);
            return View(availabilities.ToList());
        }

        // GET: availabilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            availability availability = db.availabilities.Find(id);
            if (availability == null)
            {
                return HttpNotFound();
            }
            return View(availability);
        }

        // GET: availabilities/Create
        [Authorize(Roles ="Staff")]
        public ActionResult Create()
        {
            ViewBag.field_id = new SelectList(db.avl_field, "field_id", "field_id");
            return View();
        }

        // POST: availabilities/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "field_id,start_time,booked")] availability availability)
        {
            if (ModelState.IsValid)
            {
                db.availabilities.Add(availability);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.field_id = new SelectList(db.avl_field, "field_id", "field_id", availability.field_id);
            return View(availability);
        }

        // GET: availabilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            availability availability = db.availabilities.Find(id);
            if (availability == null)
            {
                return HttpNotFound();
            }
            ViewBag.field_id = new SelectList(db.avl_field, "field_id", "field_id", availability.field_id);
            return View(availability);
        }

        // POST: availabilities/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "field_id,start_time,booked")] availability availability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(availability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.field_id = new SelectList(db.avl_field, "field_id", "field_id", availability.field_id);
            return View(availability);
        }

        // GET: availabilities/Delete/5
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            availability availability = db.availabilities.Find(id);
            if (availability == null)
            {
                return HttpNotFound();
            }
            return View(availability);
        }

        // POST: availabilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            availability availability = db.availabilities.Find(id);
            db.availabilities.Remove(availability);
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
