using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
        private DataUserAndRequest data2 = new DataUserAndRequest();
        public RequestsController()
        {
            UserStore<Account> userStore = new UserStore<Account>(db);
            userManager = new UserManager<Account>(userStore);
        }
        public ActionResult Assginee()
        {
            string curentuserid = User.Identity.GetUserId();
            Account currentUser = db.Users.FirstOrDefault(x => x.Id == curentuserid);
            var requests = db.Requests.Where(r => r.FacilityCategory_Id == currentUser.FacilityCategory_Id && r.Status==Requests.RequestStatus.Assigned);
            return View(requests.ToList());
        }
        public ActionResult DetailForAss(int id)
        {
            data2.Request = db.Requests.Find(id);
            data2.Accounts = db.Users.Where(a => a.Id == data2.Request.AssgineeId).ToList();
            Requests requests = db.Requests.Find(id);
            return View(data2);
        }
        public ActionResult DetailsForAss([Bind(Include = "RequestId")] Requests request)
        {
            var exist = db.Requests.Find(request.RequestId);
            if (exist == null)
            {
                return HttpNotFound();
            }
            exist.Updated_At = DateTime.Now;
            exist.Status = Requests.RequestStatus.Processing;
            db.Requests.AddOrUpdate(exist);
            db.SaveChanges();
            return Redirect("/Admin/Requests/DetailForAss");

        }
        // GET: Admin/Requests
        public ActionResult Index()
        {
            string curentuserid = User.Identity.GetUserId();
            Account currentUser = db.Users.FirstOrDefault(x => x.Id == curentuserid);
            var Requestorid = currentUser.Id;
            var requests = db.Requests.Where(s => s.RequestorId == Requestorid);
            return View(requests.ToList());
        }

        public ActionResult Facility()
        {
            var re = db.Requests.Where(r=>r.Status==Requests.RequestStatus.Waiting).ToList();
            return View(re);
        }
        public ActionResult DetailForFacility(int id)
        {

            data2.Request = db.Requests.Find(id);
            data2.Accounts = db.Users.Where(u => u.FacilityCategory_Id == data2.Request.FacilityCategory_Id).ToList();

            return View(data2);
        }
        [HttpPost]
        public ActionResult DetailsForFacility([Bind(Include = "RequestId,AssgineeId")] Requests request)
        {
            var exist = db.Requests.Find(request.RequestId);
            if (exist==null)
            {
                return HttpNotFound();
            }
            var assignor = db.Users.Find(request.AssgineeId);
            if (assignor==null)
            {
                return HttpNotFound();
            }  
                exist.AssgineeId = request.AssgineeId;
                exist.Updated_At = DateTime.Now;
                exist.Status = Requests.RequestStatus.Assigned;
                db.Requests.AddOrUpdate(exist);
                db.SaveChanges();
                return Redirect("/Admin/Requests/Facility");
            
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
                Status = Requests.RequestStatus.Waiting

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
