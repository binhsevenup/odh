using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using OHD_Project_Sem_3.App_Start;
using OHD_Project_Sem_3.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OHD_Project_Sem_3.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private MyContext dbContext = new MyContext();
        private UserManager<Account> userManager;
        // GET: Admin/Account
        public AccountController()
        {
            UserStore<Account> userStore = new UserStore<Account>(dbContext);
            userManager = new UserManager<Account>(userStore);
        }

        public ActionResult Index(string[] ids, string[] roleNames)
        {
            foreach (var id in ids)
            {
                userManager.AddToRoles(id, roleNames);
            }
            Account acc = dbContext.Users.Find("");
            return View("Register");
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                
            
            Account account = userManager.Find(username, password);
            if (account == null)
            {
                Danger("The email address or password is incorrect, please try again.", true);
                return View("Login");

            }
            // success
            var ident = userManager.CreateIdentity(account, DefaultAuthenticationTypes.ApplicationCookie);
            //use the instance that has been created. 
            var authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignIn(
                new AuthenticationProperties { IsPersistent = false }, ident);
            }
            Success("Login success!");
            return Redirect("/Home");
        }


        // GET: Accounts
        public ActionResult Register()
        {         
            return View(dbContext.FacilityCategories.ToList());
        }

        

        [HttpPost]
        public async Task<ActionResult>Store(string username, string password,string role,string ass,int phone,string fullname)
        {


            if (ModelState.IsValid)
            {
                var account = new Account()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = username,
                Created_At=DateTime.Now,
                Update_At=DateTime.Now,
                FacilityCategory_Id=ass,
                Email=username,
                Phone=phone,
                FullName=fullname,

            };
            
                var result = await userManager.CreateAsync(account, password);
            if (result.Succeeded)
            {
                userManager.AddToRole(account.Id, role);
                Success("Register success!", true);
                return RedirectToAction("Login", "Account");
            }
            else
            {
                Debug.WriteLine(result.Errors);
            }
            }
            Danger("Error, please try again!");
            return View("Register");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}