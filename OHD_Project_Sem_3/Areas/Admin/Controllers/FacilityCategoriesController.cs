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
    public class FacilityCategoriesController : BaseController
    {
        private MyContext db = new MyContext();

        // GET: Admin/FacilityCategories
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FaciCategoryNameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var facilitiesCategory = from f in db.Facilities
                                     select f;
            if (!String.IsNullOrEmpty(searchString))
            {
                facilitiesCategory = facilitiesCategory.Where(f => f.FacilityName.Contains(searchString)
                                                   || f.FacilityId.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    facilitiesCategory = facilitiesCategory.OrderByDescending(f => f.FacilityName);
                    break;
                case "Date":
                    facilitiesCategory = facilitiesCategory.OrderBy(f => f.Created_At);
                    break;
                case "date_desc":
                    facilitiesCategory = facilitiesCategory.OrderByDescending(f => f.Created_At);
                    break;
                default:
                    facilitiesCategory = facilitiesCategory.OrderBy(f => f.Updated_At);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(facilitiesCategory.ToPagedList(pageNumber, pageSize));
        }

        // GET: Admin/FacilityCategories/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                Warning("The record does not exist.", true);
                return View("Index");
            }
            FacilityCategory facilityCategory = db.FacilityCategories.Find(id);
            if (facilityCategory == null)
            {
                Warning("The record does not exist.", true);
                return View("Index");
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
                //                TempData["Msg"] = "Add Category Success!";
                Success("Add category success!", true);




                return RedirectToAction("Index");
            }
            Danger("Looks like something went wrong. Please check your form.");

            return View(facilityCategory);
        }

        // GET: Admin/FacilityCategories/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                Warning("The record does not exist.", true);
                return View("Index");
            }
            FacilityCategory facilityCategory = db.FacilityCategories.Find(id);
            if (facilityCategory == null)
            {
                Warning("The record does not exist.", true);
                return View("Index");
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
            if (facilityCategory.FacilityCategory_Id == null)
            {
                Warning("The record does not exist.", true);
                return View("Index");
            }

            var existCategory = db.FacilityCategories.Find(facilityCategory.FacilityCategory_Id);
            if (existCategory == null)
            {
                Warning("The record does not exist.", true);
                return View("Index");
            }
            if (ModelState.IsValid)
            {
                existCategory.Updated_At = DateTime.Now;
                existCategory.FacilityCategory_Id = facilityCategory.FacilityCategory_Id;
                existCategory.FacilityCategory_Name = facilityCategory.FacilityCategory_Name;
                db.FacilityCategories.AddOrUpdate(existCategory);
                db.SaveChanges();
                Success("Edit category success!", true);
                return RedirectToAction("Index");
            }
            Danger("An error occurred while editing, please try again later.", true);
            return View(facilityCategory);
        }

        // GET: Admin/FacilityCategories/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                Warning("Category does not exist, please check again!");
                return View("Index");
            }
            FacilityCategory facilityCategory = db.FacilityCategories.Find(id);
            if (facilityCategory == null)
            {
                Warning("Category does not exist, please check again!");
                return View("Index");
            }
            return View(facilityCategory);
        }

        // POST: Admin/FacilityCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {


                FacilityCategory facilityCategory = db.FacilityCategories.Find(id);
                db.FacilityCategories.Remove(facilityCategory);
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
