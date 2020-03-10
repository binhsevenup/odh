using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OHD_Project_Sem_3.Models
{
    public class Request : IEnumerable
    {
        [Key]
        public int RequestId { get; set; }

        public string Message { get; set; }
        public string Category { get; set; }
        public string Facility { get; set; }
        public DateTime CreatedAt { get; set; }
        public RequestStatus Status { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    public enum RequestStatus
    {
    Done = 1,
    Deleted = -1,
    Processing = 1
    }
}