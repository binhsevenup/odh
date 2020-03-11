using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Areas.Admin.Models
{
    public class Facility
    {
        [Key]
        public string FacilityId { get; set; }
        public string FacilityName { get; set; }
        public string Description { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public virtual FacilityCategory FacilityCategory { get; set; }
        public string FacilityCategory_Id { get; set; }
        public FancilitySatus Status { get; set; }
        public enum FancilitySatus {
            Active = 1,
            Deactive = 0,
            Delete = -1
        }
    }
}