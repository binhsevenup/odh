using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Areas.Admin.Models
{
    public class FacilityCategory
    {
        [Key]
        public string FacilityCategory_Id { get; set; }
        public string FacilityCategory_Name { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public FacilityCategoryStatus Status { get; set; }
        public enum FacilityCategoryStatus {
            Active = 1,
            Deactive = 0,
            Delete = -1
        }
        public virtual ICollection<Facility> Facilities { get; set; }
        public virtual ICollection<Requests> Requests { get; set; }
    }
}