using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Areas.Admin.Models
{
    public class Alert
    {
        public const string TempDataKey = "TempDataAlerts";

        public string AlertStyle { get; set; }
        public string Message { get; set; }
        public bool Dismissable { get; set; }
    }

    public static class AlertStyles
    {
        public const string Success = "alert-success";
        public const string Information = "alert-info";
        public const string Warning = "alert-warning";
        public const string Danger = "alert-danger";
    }
}