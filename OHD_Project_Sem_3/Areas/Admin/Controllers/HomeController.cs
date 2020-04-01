using OHD_Project_Sem_3.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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


            var TopAccountsSendRequest12 = db.Requests.Where(r => r.Created_At.Month == month).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();
            string Id12;

            if (TopAccountsSendRequest12.Count()!=0)
            {
                Id12 = TopAccountsSendRequest12[0];                               
            }
            else
            {
                Id12 = "0";
            }
            var Top12 = db.Requests.Where(t => t.RequestorId == Id12).Count();

            var TopAccountsSendRequest1 = db.Requests.Where(r => r.Created_At.Month == month).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id1;

            if (TopAccountsSendRequest1.Count() != 0)
            {
                Id1 = TopAccountsSendRequest1[0];
            }
            else
            {
                Id1 = "0";
            }
            var Top1 = db.Requests.Where(t => t.RequestorId == Id1).Count();

            var TopAccountsSendRequest2 = db.Requests.Where(r => r.Created_At.Month == month).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id2;

            if (TopAccountsSendRequest2.Count() != 0)
            {
                Id2 = TopAccountsSendRequest2[0];
            }
            else
            {
                Id2 = "0";
            }
            var Top2 = db.Requests.Where(t => t.RequestorId == Id2).Count();

            var TopAccountsSendRequest3 = db.Requests.Where(r => r.Created_At.Month == month - 9).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(3)
                                    .Select(g => g.Key).ToList();

            string Id3;

            if (TopAccountsSendRequest3.Count() != 0)
            {
                Id3 = TopAccountsSendRequest3[0];
            }
            else
            {
                Id3 = "0";
            }
            var Top3 = db.Requests.Where(t => t.RequestorId == Id3).Count();

            var TopAccountsSendRequest4 = db.Requests.Where(r => r.Created_At.Month == month - 8).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();
            string Id4;
            if (TopAccountsSendRequest4.Count() != 0)
            {
                Id4 = TopAccountsSendRequest4[0];               
            }
            else
            {
                Id4 = "0";
            }
            var Top4 = db.Requests.Where(t => t.RequestorId == Id4).Count();

            var TopAccountsSendRequest5 = db.Requests.Where(r => r.Created_At.Month == month - 7).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id5;

            if (TopAccountsSendRequest5.Count() != 0)
            {
                Id5 = TopAccountsSendRequest5[0];
            }
            else
            {
                Id5 = "0";
            }
            var Top5 = db.Requests.Where(t => t.RequestorId == Id5).Count();

            var TopAccountsSendRequest6 = db.Requests.Where(r => r.Created_At.Month == month - 6).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id6;

            if (TopAccountsSendRequest6.Count() != 0)
            {
                Id6 = TopAccountsSendRequest6[0];
            }
            else
            {
                Id6 = "0";
            }
            var Top6 = db.Requests.Where(t => t.RequestorId == Id6).Count();

            var TopAccountsSendRequest7 = db.Requests.Where(r => r.Created_At.Month == month - 5).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id7;

            if (TopAccountsSendRequest7.Count() != 0)
            {
                Id7 = TopAccountsSendRequest7[0];
            }
            else
            {
                Id7 = "0";
            }
            var Top7 = db.Requests.Where(t => t.RequestorId == Id7).Count();

            var TopAccountsSendRequest8 = db.Requests.Where(r => r.Created_At.Month == month - 4).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id8;

            if (TopAccountsSendRequest8.Count() != 0)
            {
                Id8 = TopAccountsSendRequest8[0];
            }
            else
            {
                Id8 = "0";
            }
            var Top8 = db.Requests.Where(t => t.RequestorId == Id8).Count();

            var TopAccountsSendRequest9 = db.Requests.Where(r => r.Created_At.Month == month - 3).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id9;

            if (TopAccountsSendRequest9.Count() != 0)
            {
                Id9 = TopAccountsSendRequest9[0];
            }
            else
            {
                Id9 = "0";
            }
            var Top9 = db.Requests.Where(t => t.RequestorId == Id9).Count();

            var TopAccountsSendRequest10 = db.Requests.Where(r => r.Created_At.Month == month - 2).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id10;

            if (TopAccountsSendRequest10.Count() != 0)
            {
                Id10 = TopAccountsSendRequest10[0];
            }
            else
            {
                Id10 = "0";
            }
            var Top10 = db.Requests.Where(t => t.RequestorId == Id10).Count();

            var TopAccountsSendRequest11 = db.Requests.Where(r => r.Created_At.Month == month - 1).ToList()
                                    .GroupBy(q => q.RequestorId)
                                    .OrderByDescending(gp => gp.Count())
                                    .Take(1)
                                    .Select(g => g.Key).ToList();

            string Id11;

            if (TopAccountsSendRequest11.Count() != 0)
            {
                Id11 = TopAccountsSendRequest11[0];
            }
            else
            {
                Id11 = "0";
            }
            var Top11 = db.Requests.Where(t => t.RequestorId == Id11).Count();


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

            ViewBag.Top1 = Top1;
            ViewBag.Top2 = Top2;
            ViewBag.Top3 = Top3;
            ViewBag.Top4 = Top4;
            ViewBag.Top5 = Top5;
            ViewBag.Top6 = Top6;
            ViewBag.Top7 = Top7;
            ViewBag.Top8 = Top8;
            ViewBag.Top9 = Top9;
            ViewBag.Top10 = Top10;
            ViewBag.Top11 = Top11;
            ViewBag.Top12 = Top12;




            if (totalrequestsdone == 0)
            {
                ViewBag.requestdone = 0;
            }
            else
            {
                ViewBag.requestdone = totalrequestsdone * 100 / totalrequets;

            }
            if (totalrequestsass == 0)
            {
                ViewBag.requestass = 0;
            }
            else
            {
                ViewBag.requestass = totalrequestsass * 100 / totalrequets;
            }
            if (totalrequestswting == 0)
            {
                ViewBag.requestswting = 0;
            }
            else
            {
                ViewBag.requestswting = totalrequestswting * 100 / totalrequets;
            }

            if (totalrequestsdeleted == 0)
            {
                ViewBag.requestdt = 0;
            }
            else
            {
                ViewBag.requestdt = totalrequestsdeleted * 100 / totalrequets;
            }

            if (totalrequestspro == 0)
            {
                ViewBag.requestpro = 0;
            }
            else
            {
                ViewBag.requestpro = totalrequestspro * 100 / totalrequets;
            }

            ViewBag.AccountsActive = accAT;
            ViewBag.AccountDeActive = accDAT;

            ViewBag.RequestsDone = rqdone;
            ViewBag.RequestNoDone = rqnodone;



            return View();
        }
    }
}