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
    public class avl_fieldController : Controller
    {
        private defaultConnectionEntities db = new defaultConnectionEntities();

        // GET: avl_field
        public ActionResult Index()
        {
            var avl_field = db.avl_field.Include(a => a.availability).Include(a => a.court);
            return View(avl_field.ToList());
        }

        // GET: avl_field/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            avl_field avl_field = db.avl_field.Find(id);
            if (avl_field == null)
            {
                return HttpNotFound();
            }
            return View(avl_field);
        }

        // GET: avl_field/Create
        [Authorize(Roles = "Admin")]

        public ActionResult Create()
        {
            ViewBag.field_id = new SelectList(db.availabilities, "field_id", "field_id");
            ViewBag.court_id = new SelectList(db.courts, "court_id", "court_name");
            return View();
        }

        // POST: avl_field/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "field_id,court_id")] avl_field avl_field)
        {
            if (ModelState.IsValid)
            {
                db.avl_field.Add(avl_field);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.field_id = new SelectList(db.availabilities, "field_id", "field_id", avl_field.field_id);
            ViewBag.court_id = new SelectList(db.courts, "court_id", "court_name", avl_field.court_id);
            return View(avl_field);
        }

        // GET: avl_field/Edit/5
        [Authorize(Roles = "Staff")]

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            avl_field avl_field = db.avl_field.Find(id);
            if (avl_field == null)
            {
                return HttpNotFound();
            }
            ViewBag.field_id = new SelectList(db.availabilities, "field_id", "field_id", avl_field.field_id);
            ViewBag.court_id = new SelectList(db.courts, "court_id", "court_name", avl_field.court_id);
            return View(avl_field);
        }

        // POST: avl_field/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Staff")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "field_id,court_id")] avl_field avl_field)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avl_field).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.field_id = new SelectList(db.availabilities, "field_id", "field_id", avl_field.field_id);
            ViewBag.court_id = new SelectList(db.courts, "court_id", "court_name", avl_field.court_id);
            return View(avl_field);
        }

        // GET: avl_field/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            avl_field avl_field = db.avl_field.Find(id);
            if (avl_field == null)
            {
                return HttpNotFound();
            }
            return View(avl_field);
        }

        // POST: avl_field/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            avl_field avl_field = db.avl_field.Find(id);
            db.avl_field.Remove(avl_field);
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
