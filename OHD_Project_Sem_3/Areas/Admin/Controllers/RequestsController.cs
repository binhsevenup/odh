using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OHD_Project_Sem_3.Areas.Admin.Models;

namespace OHD_Project_Sem_3.Areas.Admin.Controllers
{
    public class RequestsController : BaseController
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
            var requests = db.Requests.Where(r => r.FacilityCategory_Id == currentUser.FacilityCategory_Id && r.Status == Requests.RequestStatus.Assigned);
            return View(requests.ToList());
        }
        public ActionResult DetailForAss(int id)
        {
            data2.Request = db.Requests.Find(id);
            data2.Accounts = db.Users.Where(a => a.Id == data2.Request.AssgineeId).ToList();
            Requests requests = db.Requests.Find(id);
            return View(data2);
        }
        public ActionResult ConfirmReturneds()
        {
            string curentuserid = User.Identity.GetUserId();
            Account currentUser = db.Users.FirstOrDefault(x => x.Id == curentuserid);
            var confirm = db.Requests.Where(c => c.Status == Requests.RequestStatus.Processing && c.FacilityCategory_Id == currentUser.FacilityCategory_Id);
            return View(confirm.ToList());
        }
        public ActionResult ConfirmReturned([Bind(Include = "RequestId,FacilityId")] Requests request)
        {
            var requests = db.Requests.Find(request.RequestId);
            var facility = db.Facilities.Find(request.FacilityId);
            facility.Status = Models.Facility.FancilitySatus.Active;
            requests.Status = Requests.RequestStatus.Done;
            db.Requests.AddOrUpdate(requests);
            db.Facilities.AddOrUpdate(facility);
            db.SaveChanges();
            return Redirect("/Admin/Requests/Assginee");
        }
        public ActionResult DetailForConfirmReturned(int id)
        {
            data2.Request = db.Requests.Find(id);
            data2.Accounts = db.Users.Where(a => a.Id == data2.Request.AssgineeId).ToList();
            Requests requests = db.Requests.Find(id);
            return View(data2);
        }
        public ActionResult DetailsForAss([Bind(Include = "RequestId,FacilityId")] Requests request)
        {
            var exist = db.Requests.Find(request.RequestId);
            var facility = db.Facilities.Find(request.FacilityId);
            if (exist == null)
            {
                return HttpNotFound();
            }
            facility.Status = Models.Facility.FancilitySatus.Deactive;
            exist.Updated_At = DateTime.Now;
            exist.Status = Requests.RequestStatus.Processing;
            db.Requests.AddOrUpdate(exist);
            db.Facilities.AddOrUpdate(facility);
            db.SaveChanges();
            string email = "sieuphamyasuo393@gmail.com";
            string password = "muxcbqdsyjjhbkbq";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(exist.Requestor.Email));
            msg.Subject = "Request has been Assginor confirm !!! ";
            msg.Body = "Your request has ID:" + exist.RequestId + "has been Assginor confirm !!!";
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
            return Redirect("/Admin/Requests/Assginee");

        }
        public ActionResult Facility()
        {
            var re = db.Requests.Where(r => r.Status == Requests.RequestStatus.Waiting).ToList();
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
            if (exist == null)
            {
                return HttpNotFound();
            }
            var assignor = db.Users.Find(request.AssgineeId);
            if (assignor == null)
            {
                return HttpNotFound();
            }
            exist.AssgineeId = request.AssgineeId;
            exist.Updated_At = DateTime.Now;
            exist.Status = Requests.RequestStatus.Assigned;
            db.Requests.AddOrUpdate(exist);
            db.SaveChanges();
            var emailto = assignor.Email;
            string email = "sieuphamyasuo393@gmail.com";
            string password = "muxcbqdsyjjhbkbq";

            var loginInfo = new NetworkCredential(email, password);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);

            msg.From = new MailAddress(email);
            msg.To.Add(new MailAddress(emailto));
            msg.Subject = "Request has been Assginor confirm !!! ";
            msg.Body = "The request with id:" + exist.RequestId + "is waiting for your confirmation !!!";
            msg.IsBodyHtml = true;

            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
            return Redirect("/Admin/Requests/Facility");

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
            data.Facilities = db.Facilities.Where(f => f.Status == Models.Facility.FancilitySatus.Active).ToList();

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
            if (ModelState.IsValid)
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
                Success("Send request success!", true);
                //var idRole = db.Roles.Where(r => r.Name == "Facility-Heads");

                //string email = "sieuphamyasuo393@gmail.com";
                //string password = "muxcbqdsyjjhbkbq";

                //var loginInfo = new NetworkCredential(email, password);
                //var msg = new MailMessage();
                //var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                //msg.From = new MailAddress(email);
                //msg.To.Add(new MailAddress(request.Requestor.Email));
                //msg.Subject = "Request has been Assginor confirm !!! ";
                //msg.Body = "Your request has ID:" + request.RequestId + "has been Assginor confirm !!!";
                //msg.IsBodyHtml = true;

                //smtpClient.EnableSsl = true;
                //smtpClient.UseDefaultCredentials = false;
                //smtpClient.Credentials = loginInfo;
                //smtpClient.Send(msg);
                return RedirectToAction("Index", "Requests");
            }
            Danger("Error, please try again!", true);
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
            var existRequest = db.Requests.Find(request.RequestId);
            if (existRequest == null)
            {
                Warning("Request not found.");
                return View("Index");
            }
            if (ModelState.IsValid)
            {
                existRequest.RequestId = request.RequestId;
                existRequest.FacilityId = request.FacilityId;
                existRequest.FacilityCategory_Id = request.FacilityCategory_Id;
                existRequest.AssgineeId = request.AssgineeId;
                existRequest.Remarks = request.Remarks;
                existRequest.Updated_At = DateTime.Now;
                existRequest.RequestorId = request.RequestorId;
                existRequest.Status = request.Status;

                db.Requests.AddOrUpdate(existRequest);
                db.SaveChanges();
                Success("Edit request success!");
                return RedirectToAction("Index");
            }
            ViewBag.FacilityId = new SelectList(db.Facilities, "FacilityId", "FacilityName", request.FacilityId);
            ViewBag.FacilityCategory_Id = new SelectList(db.FacilityCategories, "FacilityCategory_Id", "FacilityCategory_Name", request.FacilityCategory_Id);
            Danger("Error, please try again.");
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
