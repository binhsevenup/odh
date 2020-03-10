using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OHD_Project_Sem_3.Models;

namespace OHD_Project_Sem_3.Controllers
{
    public class HomeController : Controller
    {
        private MyDbContext dbContext = new MyDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Store( string message, string category, string facility )
        {
            var request = new Request
            {
                
                Message = message,
                Category = category,
                Facility = facility,
                CreatedAt = DateTime.Now,
                Status = RequestStatus.Processing
            };
            dbContext.Requests.Add(request);
            dbContext.SaveChanges();
            return Redirect("/Home/ListRequest");
        }
    }
}