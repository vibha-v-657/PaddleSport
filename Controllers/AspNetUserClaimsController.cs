﻿using System;
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
    public class AspNetUserClaimsController : Controller
    {
        private defaultConnectionEntities db = new defaultConnectionEntities();

        // GET: AspNetUserClaims
        public ActionResult Index()
        {
            var aspNetUserClaims = db.AspNetUserClaims.Include(a => a.AspNetUser);
            return View(aspNetUserClaims.ToList());
        }

        // GET: AspNetUserClaims/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserClaim aspNetUserClaim = db.AspNetUserClaims.Find(id);
            if (aspNetUserClaim == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserClaim);
        }

        // GET: AspNetUserClaims/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: AspNetUserClaims/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserId,ClaimType,ClaimValue")] AspNetUserClaim aspNetUserClaim)
        {
            if (ModelState.IsValid)
            {
                db.AspNetUserClaims.Add(aspNetUserClaim);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaim.UserId);
            return View(aspNetUserClaim);
        }

        // GET: AspNetUserClaims/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserClaim aspNetUserClaim = db.AspNetUserClaims.Find(id);
            if (aspNetUserClaim == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaim.UserId);
            return View(aspNetUserClaim);
        }

        // POST: AspNetUserClaims/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserId,ClaimType,ClaimValue")] AspNetUserClaim aspNetUserClaim)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aspNetUserClaim).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.AspNetUsers, "Id", "Email", aspNetUserClaim.UserId);
            return View(aspNetUserClaim);
        }

        // GET: AspNetUserClaims/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AspNetUserClaim aspNetUserClaim = db.AspNetUserClaims.Find(id);
            if (aspNetUserClaim == null)
            {
                return HttpNotFound();
            }
            return View(aspNetUserClaim);
        }

        // POST: AspNetUserClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AspNetUserClaim aspNetUserClaim = db.AspNetUserClaims.Find(id);
            db.AspNetUserClaims.Remove(aspNetUserClaim);
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
