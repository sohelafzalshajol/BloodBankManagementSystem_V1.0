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
    public class RequisitionsController : Controller
    {
        private BBEntities db = new BBEntities();

        // GET: Requisitions
        public ActionResult Index()
        {
            var requisitions = db.Requisitions.Include(r => r.BloodGroup).Include(r => r.District).Include(r => r.Thana);
            return View(requisitions.ToList());
        }

        // GET: Requisitions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = db.Requisitions.Find(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            return View(requisition);
        }

        // GET: Requisitions/Create
        public ActionResult Create()
        {
            ViewBag.Group_ID = new SelectList(db.BloodGroups, "ID", "Name");
            ViewBag.District_ID = new SelectList(db.Districts, "DistrictID", "DistrictName");
            ViewBag.Thana_ID = new SelectList(db.Thanas, "Thana_ID", "ThanaName");
            return View();
        }

        // POST: Requisitions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Req_ID,Name,Group_ID,District_ID,Thana_ID,ContactNumber,Email,Req_Date,Status,Message")] Requisition requisition)
        {
            if (ModelState.IsValid)
            {
                db.Requisitions.Add(requisition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Group_ID = new SelectList(db.BloodGroups, "ID", "Name", requisition.Group_ID);
            ViewBag.District_ID = new SelectList(db.Districts, "DistrictID", "DistrictName", requisition.District_ID);
            ViewBag.Thana_ID = new SelectList(db.Thanas, "Thana_ID", "ThanaName", requisition.Thana_ID);
            return View(requisition);
        }

        // GET: Requisitions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = db.Requisitions.Find(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            ViewBag.Group_ID = new SelectList(db.BloodGroups, "ID", "Name", requisition.Group_ID);
            ViewBag.District_ID = new SelectList(db.Districts, "DistrictID", "DistrictName", requisition.District_ID);
            ViewBag.Thana_ID = new SelectList(db.Thanas, "Thana_ID", "ThanaName", requisition.Thana_ID);
            return View(requisition);
        }

        // POST: Requisitions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Req_ID,Name,Group_ID,District_ID,Thana_ID,ContactNumber,Email,Req_Date,Status,Message")] Requisition requisition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requisition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Group_ID = new SelectList(db.BloodGroups, "ID", "Name", requisition.Group_ID);
            ViewBag.District_ID = new SelectList(db.Districts, "DistrictID", "DistrictName", requisition.District_ID);
            ViewBag.Thana_ID = new SelectList(db.Thanas, "Thana_ID", "ThanaName", requisition.Thana_ID);
            return View(requisition);
        }

        // GET: Requisitions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Requisition requisition = db.Requisitions.Find(id);
            if (requisition == null)
            {
                return HttpNotFound();
            }
            return View(requisition);
        }

        // POST: Requisitions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requisition requisition = db.Requisitions.Find(id);
            db.Requisitions.Remove(requisition);
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
