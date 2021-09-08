using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UsersRestService.Models
{
    public class UserMode
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}