using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentationLayer.Models
{
    public class LevelViewModel
    {
        public int LevelID { get; set; }
        public DateTime Year { get; set; }
        public string Semester { get; set; }
        public string Description { get; set; }
    }
}