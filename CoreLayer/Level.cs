using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public class Level
    {
        public Level()
        {
            Student = new List<Student>();
            Course = new List<Course>();
        }

        public int LevelID { get; set; }
        public DateTime Year { get; set; }
        public string Semester { get; set; }
        public string Description { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }
}
