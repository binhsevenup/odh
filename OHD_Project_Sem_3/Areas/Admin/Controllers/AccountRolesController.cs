using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OHD_Project_Sem_3.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OHD_Project_Sem_3.Areas.Admin.Controllers
{
    public class AccountRolesController : BaseController
    {
        private MyContext dbContext = new MyContext();
        private RoleManager<AccountRole> roleManager;

        public AccountRolesController()
        {
            RoleStore<AccountRole> roleStore = new RoleStore<AccountRole>(dbContext);
            roleManager = new RoleManager<AccountRole>(roleStore);
        }
        // GET: AccountRoles
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View(dbContext.Roles.ToList());
        }

        [HttpPost]
        public ActionResult Store(string name)
        {
            if (ModelState.IsValid)
            {
                
            
            var role = new AccountRole()
            {
                Name = name,
                
            };
            var result = roleManager.Create(role);
            Success("Add role success!");
            return RedirectToAction("Index", "AccountRoles");
            }
            Danger("Error, please try again!");
            return Redirect("/Admin/AccountRoles");
        }
    }
}