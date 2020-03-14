using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Areas.Admin.Models
{
    public class TotalData
    {
        
        public List<Facility> Facilities;
        
        public List<FacilityCategory> FacilityCategories;
    }
}