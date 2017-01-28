using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class CourseViewModel
    {
        public string CourseID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string Level { get; set; }
        public string Duration { get; set; }
        public string Credit { get; set; }
        public string Description { get; set; }
    }
}