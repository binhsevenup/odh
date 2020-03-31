using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using OHD_Project_Sem_3.App_Start;
using OHD_Project_Sem_3.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
        private AccountsAndFacilityCategory a = new AccountsAndFacilityCategory();
        // GET: Admin/Account
        public AccountController()
        {
            UserStore<Account> userStore = new UserStore<Account>(dbContext);
            userManager = new UserManager<Account>(userStore);
        }

        public ActionResult Index()
        {
            var accounts = dbContext.Users.ToList();
            return View(accounts);
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
            return Redirect("/Admin/Home");
        }


        // GET: Accounts
        public ActionResult Register()
        {
            a.FacilityCategories = dbContext.FacilityCategories.ToList();
            a.AccountRoles = dbContext.Roles.ToList();
            return View(a);
        }



        [HttpPost]
        public async Task<ActionResult> Store(string username, string password, string role, string ass, int phone, string fullname, string r)
        {


            if (ModelState.IsValid)
            {
                var account = new Account()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = username,
                    Created_At = DateTime.Now,
                    Update_At = DateTime.Now,
                    FacilityCategory_Id = ass,
                    Email = username,
                    Phone = phone,
                    FullName = fullname,
                    AccountRoleId = r,
                };

                var result = await userManager.CreateAsync(account, password);
                var roles = dbContext.Roles.Find(r);
                var namerole = roles.Name;
                if (result.Succeeded)
                {
                    userManager.AddToRole(account.Id, namerole);

                    Success("Register success!", true);
                    var emailto = username;
                    string email = "sieuphamyasuo393@gmail.com";
                    string password1 = "muxcbqdsyjjhbkbq";

                    var loginInfo = new NetworkCredential(email, password1);
                    var msg = new MailMessage();
                    var smtpClient = new SmtpClient("smtp.gmail.com", 587);

                    msg.From = new MailAddress(email);
                    msg.To.Add(new MailAddress(emailto));
                    msg.Subject = "Request has been Assginor confirm !!! ";
                    msg.Body = "Successful registration for account: "+emailto+ " with password: "+password;
                    msg.IsBodyHtml = true;

                    smtpClient.EnableSsl = true;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = loginInfo;
                    smtpClient.Send(msg);
                    return RedirectToAction("Login", "Account");
                }  
            }
            Danger("Error, please try again!", true);
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