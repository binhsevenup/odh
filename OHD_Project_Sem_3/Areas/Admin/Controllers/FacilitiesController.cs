using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OHD_Project_Sem_3.Areas.Admin.Models;
using PagedList;


namespace OHD_Project_Sem_3.Areas.Admin.Controllers
{
    public class FacilitiesController : BaseController
    {
        private MyContext db = new MyContext();

        // GET: Admin/Facilities
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameFa = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NameCa = sortOrder == "Category" ? "category_desc" : "Category";
            ViewBag.CreatedFa = sortOrder == "Created" ? "created_desc" : "Created";
            ViewBag.UpdatedFa = sortOrder == "Updated" ? "updated_desc" : "Updated";
            ViewBag.StatusFa = sortOrder == "Status" ? "status_desc" : "Status";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var facilities = from f in db.Facilities
                             select f;
            if (!String.IsNullOrEmpty(searchString))
            {
                facilities = facilities.Where(f => f.FacilityName.Contains(searchString)
                                               || f.FacilityCategory.FacilityCategory_Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    facilities = facilities.OrderByDescending(f => f.FacilityName);
                    break;
                case "Category":
                    facilities = facilities.OrderBy(f => f.FacilityCategory.FacilityCategory_Name);
                    break;
                case "category_desc":
                    facilities = facilities.OrderByDescending(f => f.FacilityCategory.FacilityCategory_Name);
                    break;
                case "Created":
                    facilities = facilities.OrderBy(f => f.Created_At);
                    break; 
                case "created_desc":
                    facilities = facilities.OrderByDescending(f => f.Created_At);
                    break;
                case "Updated":
                    facilities = facilities.OrderBy(f => f.Updated_At);
                    break;
                case "updated_desc":
                    facilities = facilities.OrderByDescending(f => f.Updated_At);
                    break;
                case "Status":
                    facilities = facilities.OrderBy(f => f.Status);
                    break;
                case "status_desc":
                    facilities = facilities.OrderByDescending(f => f.Status);
                    break;
                default:
                    facilities = facilities.OrderBy(f => f.FacilityName);
                    break;
            }
            //            var facilities = db.Facilities.Include(f => f.FacilityCategory);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(facilities.ToPagedList(pageNumber, pageSize));
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
                facility.Created_At = DateTime.Now;
                db.Facilities.Add(facility);
                db.SaveChanges();
                Success("Add facility success!", true);
                return RedirectToAction("Index");
            }

            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name", facility.FacilityCategory_Id);
            Warning("Please enter full information.", true);
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
            if (facility.FacilityId == null)
            {
                Warning("Facility does not exist, please check again!");
                return View("Index");

            }

            var existFacility = db.Facilities.Find(facility.FacilityId);
            if (existFacility == null)
            {
                Warning("Facility does not exist, please check again!");
                return View("Index");
            }
            if (ModelState.IsValid)
            {
                existFacility.FacilityId = facility.FacilityId;
                existFacility.Status = facility.Status;
                existFacility.FacilityCategory_Id = facility.FacilityCategory_Id;
                existFacility.FacilityName = facility.FacilityName;
                existFacility.Description = facility.Description;
                existFacility.Updated_At = DateTime.Now;
                db.Facilities.AddOrUpdate(existFacility);
                db.SaveChanges();
                Success("Edit success!", true);
                return RedirectToAction("Index");
            }
            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name", facility.FacilityCategory_Id);
            Danger("An error occurred while editing, please try again later.", true);
            return View(facility);
        }

        // GET: Admin/Facilities/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                Warning("Facility does not exist, please check again!");
                return View("Index");
            }
            Facility facility = db.Facilities.Find(id);
            if (facility == null)
            {
                Warning("Facility does not exist, please check again!");
                return View("Index");
            }
            return View(facility);
        }

        // POST: Admin/Facilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                Facility facility = db.Facilities.Find(id);
                db.Facilities.Remove(facility);
                db.SaveChanges();
                Success("Delete success!", true);
                return RedirectToAction("Index");
            }
            Danger("An error occurred while deleting, please try again later", true);
            return View("Index");
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
