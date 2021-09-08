using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnroleClass.Models
{
    public class CustomClasses
    {
        public IEnumerable<Class> ClassList { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
    }
}