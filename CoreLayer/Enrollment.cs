using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public class Enrollment
    {
        public int EnID { get; set; }
        public string StudentID { get; set; }
        public int CourseID { get; set; }
        public DateTime EnrolledDate { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}
