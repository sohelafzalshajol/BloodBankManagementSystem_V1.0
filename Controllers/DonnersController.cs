using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodBankMVC.Models;

namespace BloodBankMVC.Controllers
{
    [Authorize]
    public class DonnersController : Controller
    {
        private BBEntities db = new BBEntities();

        // GET: Donners
        public ActionResult Index()
        {
            var donners = db.Donners.Include(d => d.BloodGroup).Include(d => d.District).Include(d => d.Thana);
            return View(donners.ToList());
        }

        // GET: Donners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donner donner = db.Donners.Find(id);
            if (donner == null)
            {
                return HttpNotFound();
            }
            return View(donner);
        }

        // GET: Donners/Create
        public ActionResult Create()
        {
            ViewBag.Group_ID = new SelectList(db.BloodGroups, "ID", "Name");
            ViewBag.District_ID = new SelectList(db.Districts, "DistrictID", "DistrictName");
            ViewBag.Thana_ID = new SelectList(db.Thanas, "Thana_ID", "ThanaName");
            return View();
        }

        // POST: Donners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Donner_ID,DonnerName,Group_ID,District_ID,Thana_ID,ContactNumber,Email,LastDonationDate,Status")] Donner donner)
        {
            if (ModelState.IsValid)
            {
                db.Donners.Add(donner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Group_ID = new SelectList(db.BloodGroups, "ID", "Name", donner.Group_ID);
            ViewBag.District_ID = new SelectList(db.Districts, "DistrictID", "DistrictName", donner.District_ID);
            ViewBag.Thana_ID = new SelectList(db.Thanas, "Thana_ID", "ThanaName", donner.Thana_ID);
            return View(donner);
        }

        // GET: Donners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donner donner = db.Donners.Find(id);
            if (donner == null)
            {
                return HttpNotFound();
            }
            ViewBag.Group_ID = new SelectList(db.BloodGroups, "ID", "Name", donner.Group_ID);
            ViewBag.District_ID = new SelectList(db.Districts, "DistrictID", "DistrictName", donner.District_ID);
            ViewBag.Thana_ID = new SelectList(db.Thanas, "Thana_ID", "ThanaName", donner.Thana_ID);
            return View(donner);
        }

        // POST: Donners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Donner_ID,DonnerName,Group_ID,District_ID,Thana_ID,ContactNumber,Email,LastDonationDate,Status")] Donner donner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(donner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Group_ID = new SelectList(db.BloodGroups, "ID", "Name", donner.Group_ID);
            ViewBag.District_ID = new SelectList(db.Districts, "DistrictID", "DistrictName", donner.District_ID);
            ViewBag.Thana_ID = new SelectList(db.Thanas, "Thana_ID", "ThanaName", donner.Thana_ID);
            return View(donner);
        }

        // GET: Donners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Donner donner = db.Donners.Find(id);
            if (donner == null)
            {
                return HttpNotFound();
            }
            return View(donner);
        }

        // POST: Donners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Donner donner = db.Donners.Find(id);
            db.Donners.Remove(donner);
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
