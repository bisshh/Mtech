using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer
{
    public class StudentPersonalInfo
    {
        public string StudentID { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
        public string GuardianName { get; set; }
        public string GuardianContact { get; set; }
        public string GuardianRelation { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Semester { get; set; }
        public DateTime AdmissionDate { get; set; }
        public string CitizenshipNumber { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public virtual Student Student { get; set; }
    }
}
