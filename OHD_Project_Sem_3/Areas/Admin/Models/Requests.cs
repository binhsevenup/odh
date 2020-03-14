﻿using System;
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
        public string AssgineeId { get; set; }
        public virtual FacilityCategory FacilityCategory { get; set; }
        public string FacilityCategory_Id { get; set; }
        public virtual Facility Facility{ get; set; }
        public string FacilityId { get; set; }
        public string Remarks { get; set; }
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
        public enum RequestStatus { 
            Rejected=-1,
            Closed=0,
            Unassigned=1,
            Assigned=2,
            Word_in_progress=3,
            Need_more_info=4
        }
        public RequestStatus Status { get; set; }
    }
}