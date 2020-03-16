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
        [Required(ErrorMessage = "Please enter category.")]
        [Display(Name = "Category Id")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Category Id is only allowed between 2 - 50 characters.")]
        public string FacilityCategory_Id { get; set; }
        [Required(ErrorMessage = "Please enter category name.")]
        [Display(Name = "Category Name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category Name is only allowed between 2 - 100 characters.")]
        public string FacilityCategory_Name { get; set; }
        [Display(Name = "Created At")]
        public DateTime Created_At { get; set; }
        [Display(Name = "Updated At")]
        public DateTime? Updated_At { get; set; }
        [Display(Name = "Status")]
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