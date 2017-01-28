using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public class Department
    {
        public Department()
        {
            Employee = new List<Employee>();
            Student = new List<Student>();
            Course = new List<Course>();
        }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public string DepartmentHead { get; set; }
        public DateTime? EstdDate { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual ICollection<Employee> Employee { get; set; }
        public virtual ICollection<Student> Student { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }
}
