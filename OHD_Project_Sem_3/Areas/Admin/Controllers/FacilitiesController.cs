using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OHD_Project_Sem_3.Areas.Admin.Models;

namespace OHD_Project_Sem_3.Areas.Admin.Controllers
{
    public class FacilitiesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/Facilities
        public ActionResult Index()
        {
            var facilities = db.Facilities.Include(f => f.FacilityCategory);
            return View(facilities.ToList());
        }

        // GET: Admin/Facilities/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // GET: Admin/Facilities/Create
        public ActionResult Create()
        {
            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name");
            return View();
        }

        // POST: Admin/Facilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacilityId,FacilityName,Description,Created_At,Updated_At,FacilityCategory_Id,Status")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                db.Facilities.Add(facility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name", facility.FacilityCategory_Id);
            return View(facility);
        }

        // GET: Admin/Facilities/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name", facility.FacilityCategory_Id);
            return View(facility);
        }

        // POST: Admin/Facilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacilityId,FacilityName,Description,Created_At,Updated_At,FacilityCategory_Id,Status")] Facility facility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name", facility.FacilityCategory_Id);
            return View(facility);
        }

        // GET: Admin/Facilities/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                return HttpNotFound();
            }
            return View(facility);
        }

        // POST: Admin/Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Facility facility = db.Facilities.Find(id);
            db.Facilities.Remove(facility);
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
