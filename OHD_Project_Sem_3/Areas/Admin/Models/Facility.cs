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
        [Required(ErrorMessage = "Please enter facility id.")]
        [Display(Name = "Facility Id")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Facility Id is only allowed between 2 - 50 characters.")]
        public string FacilityId { get; set; }
        [Required(ErrorMessage = "Please enter facility name.")]
        [Display(Name = "Facility Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Facility Name is only allowed between 2 - 100 characters.")]
        public string FacilityName { get; set; }
        [Required(ErrorMessage = "Please enter description.")]
        [Display(Name = "Description")]
        [StringLength(300, MinimumLength = 2, ErrorMessage = "Description is only allowed between 2 - 300 characters.")]
        public string Description { get; set; }
        [Display(Name = "Created At")]
        public DateTime Created_At { get; set; }
        [Display(Name = "Updated At")]
        public DateTime? Updated_At { get; set; }
        public virtual FacilityCategory FacilityCategory { get; set; }
        public string FacilityCategory_Id { get; set; }
        [Display(Name = "Status")]
        public FancilitySatus Status { get; set; }
        public enum FancilitySatus {
            Active = 1,
            Deactive = 0,
            Delete = -1
        }
        public virtual ICollection<Requests> Requests { get; set; }
    }
}