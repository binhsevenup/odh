using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Areas.Admin.Models
{
    public class Requests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RequestId { get; set; }
        public string RequestorId { get; set; }
        public virtual Account Requestor { get; set; }
        public virtual Account Assignor { get; set; }
        public string AssgineeId { get; set; }
        public virtual FacilityCategory FacilityCategory { get; set; }
        public string FacilityCategory_Id { get; set; }
        public virtual Facility Facility{ get; set; }
        public string FacilityId { get; set; }
        [Required(ErrorMessage = "Please enter message.")]
        [Display(Name = "Message")]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Message is only allowed between 2 - 500 characters.")]
        public string Remarks { get; set; }
        [Display(Name = "Created At")]
        public DateTime Created_At { get; set; }
        [Display(Name = "Updated At")]
        public DateTime? Updated_At { get; set; }
        public enum RequestStatus { 
            Deleted=-1,
            Done=0,
            Processing=1,
            Assigned=2,
            Waiting=3,
        }
        [Display(Name = "Status")]
        public RequestStatus Status { get; set; }
    }
}