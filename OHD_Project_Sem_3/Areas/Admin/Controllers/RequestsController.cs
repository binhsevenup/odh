using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OHD_Project_Sem_3.Areas.Admin.Models;

namespace OHD_Project_Sem_3.Areas.Admin.Controllers
{
    public class RequestsController : Controller
    {
        private MyContext db = new MyContext();
        private UserManager<Account> userManager;
        private TotalData data = new TotalData();
        public RequestsController()
        {
            UserStore<Account> userStore = new UserStore<Account>(db);
            userManager = new UserManager<Account>(userStore);
        }

        // GET: Admin/Requests
        public ActionResult Index()
        {
            var requests = db.Requests.Include(r => r.Facility).Include(r => r.FacilityCategory);
            return View(requests.ToList());
        }

        // GET: Admin/Requests/Details/5
        public ActionResult Details(int id)
        {

            Requests request = db.Requests.Find(id);
            return View(request);
        }

        // GET: Admin/Requests/Create
        public ActionResult Create()
        {
            data.FacilityCategories = db.FacilityCategories.ToList();
            data.Facilities = db.Facilities.ToList();

            ViewBag.FacilityId = new SelectList(db.Facilities, "FacilityId", "FacilityName");
            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name");
            return View(data);
        }

        // POST: Admin/Requests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult CreateStore(string category, string facility, string remarks)
        {
            string curentuserid = User.Identity.GetUserId();
            Account currentUser = db.Users.FirstOrDefault(x => x.Id == curentuserid);
            var request = new Requests()
            {
                RequestorId = curentuserid,
                FacilityCategory_Id = category,
                FacilityId = facility,
                Remarks = remarks,
                Requestor = currentUser,
                Created_At = DateTime.Now,
                Status = Requests.RequestStatus.Rejected

            };
            db.Requests.Add(request);
            db.SaveChanges();
            return Redirect("/Admin/Requests");
        }
     
        // GET: Admin/Requests/Edit/5
        public ActionResult Edit(int id)
        {
            Requests request = db.Requests.Find(id);
            ViewBag.FacilityId = new SelectList(db.Facilities, "FacilityId", "FacilityName", request.FacilityId);
            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name", request.FacilityCategory_Id);
            return View(request);
        }

        // POST: Admin/Requests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestId,RequestorId,AssgineeId,FacilityCategory_Id,FacilityId,Remarks,Created_At,Updated_At,Status")] Requests request)
        {
            if (ModelState.IsValid)
            {
                request.Updated_At = DateTime.Now;
                db.Entry(request).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FacilityId = new SelectList(db.Facilities, "FacilityId", "FacilityName", request.FacilityId);
            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name", request.FacilityCategory_Id);
            return View(request);
        }

        // GET: Admin/Requests/Delete/5
        public ActionResult Delete(int id)
        {
            Requests request = db.Requests.Find(id);

            return View(request);
        }

        // POST: Admin/Requests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Requests request = db.Requests.Find(id);
            db.Requests.Remove(request);
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
