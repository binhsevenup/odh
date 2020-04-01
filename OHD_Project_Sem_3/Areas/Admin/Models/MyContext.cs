using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Areas.Admin.Models
{
    public class MyContext : IdentityDbContext<Account>
    {
        public MyContext() : base("name=MyContext")
        {

        }

        public System.Data.Entity.DbSet<OHD_Project_Sem_3.Areas.Admin.Models.Facility> Facilities { get; set; }
        public System.Data.Entity.DbSet<OHD_Project_Sem_3.Areas.Admin.Models.FacilityCategory> FacilityCategories { get; set; }
        
        public System.Data.Entity.DbSet<OHD_Project_Sem_3.Areas.Admin.Models.Requests> Requests { get; set; }


        public System.Data.Entity.DbSet<OHD_Project_Sem_3.Areas.Admin.Models.AccountRole> IdentityRoles { get; set; }
    }
}