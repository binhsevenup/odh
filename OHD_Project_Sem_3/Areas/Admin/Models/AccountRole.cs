using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Areas.Admin.Models
{
    public class AccountRole: IdentityRole
    {
        public virtual ICollection<Account> Account { get; set; }
    }
}