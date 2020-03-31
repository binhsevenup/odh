using OHD_Project_Sem_3.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OHD_Project_Sem_3.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        private MyContext db = new MyContext();

        public ActionResult Faq()
        {
            return View();
        }
        // GET: Admin/Home
        public ActionResult Index()
        {
            var month = 12;
            //-- set from - will always be first day of current month
            DateTime dtFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

            DateTime dtFrom2 = new DateTime(DateTime.Now.Year, month, 1);

            //-- set to - current date (with 00.00.00 time)
            DateTime dtTo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);


            
            var totalrequestsdeleted = db.Requests.Where(r => r.Status == Requests.RequestStatus.Deleted).Count();
            var totalrequestsdone = db.Requests.Where(r => r.Status == Requests.RequestStatus.Done).Count();
            var totalrequestspro = db.Requests.Where(r => r.Status == Requests.RequestStatus.Processing).Count();
            var totalrequestsass = db.Requests.Where(r => r.Status == Requests.RequestStatus.Assigned).Count();
            var totalrequestswting = db.Requests.Where(r => r.Status == Requests.RequestStatus.Waiting).Count();
            var totalrequets = totalrequestsass + totalrequestsdeleted + totalrequestsdone + totalrequestspro + totalrequestswting;

            var accAT = db.Users.Where(a => a.Status == Account.AccountStatus.Active).Count();
            var accDAT = db.Users.Where(a => a.Status == Account.AccountStatus.Deactive).Count();
          
           
            var rqdone = db.Requests.Where(r => r.Status == Requests.RequestStatus.Done).Count();
            var rqnodone = db.Requests.Where(r => r.Status != Requests.RequestStatus.Done).Count();

          
               
                var requests12 = db.Requests.Where(r => r.Created_At.Month == month).Count();
            var requests11 = db.Requests.Where(r => r.Created_At.Month == month - 1).Count();
            var requests10 = db.Requests.Where(r => r.Created_At.Month == month - 2).Count();
            var requests9 = db.Requests.Where(r => r.Created_At.Month == month - 3).Count();
            var requests8 = db.Requests.Where(r => r.Created_At.Month == month - 4).Count();
            var requests7 = db.Requests.Where(r => r.Created_At.Month == month - 5).Count();
            var requests6 = db.Requests.Where(r => r.Created_At.Month == month - 6).Count();
            var requests5 = db.Requests.Where(r => r.Created_At.Month == month - 7).Count();
            var requests4 = db.Requests.Where(r => r.Created_At.Month == month - 8).Count();
            var requests3 = db.Requests.Where(r => r.Created_At.Month == month - 9).Count();
            var requests2 = db.Requests.Where(r => r.Created_At.Month == month - 10).Count();
            var requests1 = db.Requests.Where(r => r.Created_At.Month == month - 11).Count();

            var accounts12 = db.Users.Where(a => a.Created_At.Month == month).Count();
            var accounts11 = db.Users.Where(a => a.Created_At.Month == month - 1).Count();
            var accounts10 = db.Users.Where(a => a.Created_At.Month == month - 2).Count();
            var accounts9 = db.Users.Where(a => a.Created_At.Month == month - 3).Count();
            var accounts8 = db.Users.Where(a => a.Created_At.Month == month - 4).Count();
            var accounts7 = db.Users.Where(a => a.Created_At.Month == month - 5).Count();
            var accounts6 = db.Users.Where(a => a.Created_At.Month == month - 6).Count();
            var accounts5 = db.Users.Where(a => a.Created_At.Month == month - 7).Count();
            var accounts4 = db.Users.Where(a => a.Created_At.Month == month - 8).Count();
            var accounts3 = db.Users.Where(a => a.Created_At.Month == month - 9).Count();
            var accounts2 = db.Users.Where(a => a.Created_At.Month == month - 10).Count();
            var accounts1 = db.Users.Where(a => a.Created_At.Month == month - 11).Count();

            ViewBag.requests12 = requests12;
            ViewBag.requests11 = requests11;
            ViewBag.requests10 = requests10;
            ViewBag.requests9 = requests9;
            ViewBag.requests8 = requests8;
            ViewBag.requests7 = requests7;
            ViewBag.requests6 = requests6;
            ViewBag.requests5 = requests5;
            ViewBag.requests4 = requests4;
            ViewBag.requests3 = requests3;
            ViewBag.requests2 = requests2;
            ViewBag.requests1 = requests1;

            ViewBag.account = accounts1;
            ViewBag.account2 = accounts2;
            ViewBag.account3 = accounts3;
            ViewBag.account4 = accounts4;
            ViewBag.account5 = accounts5;
            ViewBag.account6 = accounts6;
            ViewBag.account7 = accounts7;
            ViewBag.account8 = accounts8;
            ViewBag.account9 = accounts9;
            ViewBag.account10 = accounts10;
            ViewBag.account11 = accounts11;
            ViewBag.account12 = accounts12;

           
            ViewBag.requestswting = totalrequestswting * 100 / totalrequets;
            ViewBag.requestpro = totalrequestspro * 100 / totalrequets;
            ViewBag.requestdt = totalrequestsdeleted * 100 / totalrequets;
            ViewBag.requestass = totalrequestsass * 100 / totalrequets;

            if (totalrequestsdone == 0)
            {
                ViewBag.requestdone = 0;
            }
            else
            {
                ViewBag.requestdone = totalrequestsdone * 100 / totalrequets;
            }

            ViewBag.AccountsActive = accAT;
            ViewBag.AccountDeActive = accDAT;

            ViewBag.RequestsDone = rqdone;
            ViewBag.RequestNoDone = rqnodone;



            return View();
        }
    }
}