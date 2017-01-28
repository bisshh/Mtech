using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class DepartmentViewModel
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public DateTime? EstdDate { get; set; }
        public string DepartmentHead { get; set; }
    }
}