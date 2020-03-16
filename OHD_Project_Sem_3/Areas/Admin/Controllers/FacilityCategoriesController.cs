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
    public class FacilityCategoriesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Admin/FacilityCategories
        public ActionResult Index()
        {
            return View(db.FacilityCategories.ToList());
        }

        // GET: Admin/FacilityCategories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityCategory facilityCategory = db.FacilityCategories.Find(id);
            if (facilityCategory == null)
            {
                return HttpNotFound();
            }
            return View(facilityCategory);
        }

        // GET: Admin/FacilityCategories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/FacilityCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FacilityCategory_Id,FacilityCategory_Name,Created_At,Updated_At,Status")] FacilityCategory facilityCategory)
        {
            if (ModelState.IsValid)
            {
                facilityCategory.Created_At = DateTime.Now;
                db.FacilityCategories.Add(facilityCategory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(facilityCategory);
        }

        // GET: Admin/FacilityCategories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityCategory facilityCategory = db.FacilityCategories.Find(id);
            if (facilityCategory == null)
            {
                return HttpNotFound();
            }
            return View(facilityCategory);
        }

        // POST: Admin/FacilityCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FacilityCategory_Id,FacilityCategory_Name,Created_At,Updated_At,Status")] FacilityCategory facilityCategory)
        {
            if (ModelState.IsValid)
            {
                facilityCategory.Updated_At = DateTime.Now;
                db.Entry(facilityCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facilityCategory);
        }

        // GET: Admin/FacilityCategories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacilityCategory facilityCategory = db.FacilityCategories.Find(id);
            if (facilityCategory == null)
            {
                return HttpNotFound();
            }
            return View(facilityCategory);
        }

        // POST: Admin/FacilityCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            FacilityCategory facilityCategory = db.FacilityCategories.Find(id);
            db.FacilityCategories.Remove(facilityCategory);
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
