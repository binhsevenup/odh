using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OHD_Project_Sem_3.Areas.Admin.Models;

namespace OHD_Project_Sem_3.Controllers
{
    public class HomeController : Controller
    {
      

        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }
}