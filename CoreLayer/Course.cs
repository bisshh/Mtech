using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public class Course
    {
        public Course()
        {
            Enrollment = new List<Enrollment>();
        }

        public int CourseID { get; set; }
        public int DepartmentID { get; set; }
        public int LevelID { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Credit { get; set; }
        public string Description { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual Department Department { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}