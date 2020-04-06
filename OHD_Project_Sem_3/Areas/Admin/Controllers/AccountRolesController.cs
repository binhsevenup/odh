using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OHD_Project_Sem_3.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace OHD_Project_Sem_3.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

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
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.RoleName = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            var roles = from f in dbContext.Roles
                select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                roles = roles.Where(f => f.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    roles = roles.OrderByDescending(f => f.Name);
                    break;
                default:
                    roles = roles.OrderBy(f => f.Name);
                    break;
            }
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(roles.ToPagedList(pageNumber, pageSize));
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