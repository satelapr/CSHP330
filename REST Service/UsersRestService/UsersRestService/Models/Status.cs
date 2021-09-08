using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersRestService.Models
{
    public class Status
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public UserMode Users { get; set; }
    }
}