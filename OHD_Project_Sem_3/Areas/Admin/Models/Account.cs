using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Areas.Admin.Models
{
    public class Account:IdentityUser
    {
        public string FullName { get; set; }
        public int Phone { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Update_At { get; set; }
        public string FacilityCategory_Id { get; set; }
        public AccountStatus Status { get; set; }
        public enum AccountStatus
        {
            Active = 1,
            Deactive = 0,
            Delete = -1 
        }

    }
}