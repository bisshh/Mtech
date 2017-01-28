using CoreLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class StudentService
    {
        StudentCRUD studentTableActions;
        StudentPersonalInfoCRUD studentpersonalinfoTableActions;

        public StudentService()
        {
            studentTableActions = new StudentCRUD();
            studentpersonalinfoTableActions = new StudentPersonalInfoCRUD();
        }

        public void SaveStudent(Student studentData)
        {
            studentTableActions.SaveStudent(studentData);
        }

        public void SaveStudent(StudentPersonalInfo studentData)
        {
            studentpersonalinfoTableActions.SaveStudent(studentData);
        }

        public Student GetStudentById(string StudentID)
        {
            return studentTableActions.GetStudentById(StudentID);
        }
        public StudentPersonalInfo GetStudentPersonalInfo(string StudentID)
        {
            return studentpersonalinfoTableActions.GetStudentPersonalInfo(StudentID);
        }
        public Student GetStudentByName(string StudentName)
        {
            return studentTableActions.GetStudentByName(StudentName);
        }
        public void UpdateStudent(Student dataToBeUpdated)
        {
            studentTableActions.UpdateStudent(dataToBeUpdated);
        }
        public void RemoveStudent(Student dataToBeDeleted)
        {
            studentTableActions.RemoveStudent(dataToBeDeleted);
        }

        public void DeleteStudent(string StudentID)
        {
            Student studentToBeDeleted = studentTableActions.GetStudentById(StudentID);
            studentTableActions.RemoveStudent(studentToBeDeleted);
        }
        public void DeleteStudentPersonalInfo(string StudentID)
        {
            StudentPersonalInfo studentToBeDeleted = studentpersonalinfoTableActions.GetStudentPersonalInfo(StudentID);
            studentpersonalinfoTableActions.RemoveStudentPersonalInfo(studentToBeDeleted);
        }
        public IList<Student> GetAllStudents(string sortOrder, string searchString)
        {
            return studentTableActions.GetAllStudents(sortOrder, searchString);
        }
        public IList<StudentPersonalInfo> GetAllStudentsPersonalInfo(string sortOrder, string searchString)
        {
            return studentpersonalinfoTableActions.GetAllStudentPersonalInfo(sortOrder, searchString);
        }
    }
}
