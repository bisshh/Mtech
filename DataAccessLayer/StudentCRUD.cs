using CoreLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class StudentCRUD
    {
        //Create Student
        public void SaveStudent(Student student)
        {
            using (var dataContext = new DataContext())
            {
                var addedStudent = dataContext.student.Add(student);
                student.StudentPersonalInfo.StudentID = addedStudent.StudentID;
                addedStudent.StudentPersonalInfo = dataContext.studentPersonalInfo.Add(student.StudentPersonalInfo);
                dataContext.SaveChanges();
            }
        }

        //Read Student
        public Student GetStudentById(string StudentID)
        {
            using (var dataContext = new DataContext())
            {
                Student student = (from Student in dataContext.student
                                   where Student.StudentID == StudentID
                                   select Student).First();
                return student;
            }
        }

        public Student GetStudentByName(string StudentName)
        {
            using (var dataContext = new DataContext())
            {
                Student student = (from Student in dataContext.student
                                   where Student.FirstName == StudentName
                                   select Student).First();
                return student;
            }
        }

        //Update Student
        public void UpdateStudent(Student newStudent)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(newStudent).State = EntityState.Modified;
                dataContext.SaveChanges();
            }
        }

        //Delete Student
        public void RemoveStudent(Student studentToBeDeleted)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(studentToBeDeleted).State = EntityState.Deleted;
                dataContext.SaveChanges();
            }
        }

        public List<Student> GetAllStudents(string sortOrder, string searchString)
        {
            using (var dataContext = new DataContext())
            {
                IEnumerable<Student> students = from Student in dataContext.student
                                                  select Student;

                if (!string.IsNullOrEmpty(searchString))
                {
                    students = students.Where(x => (x.FirstName.ToLower().Contains(searchString.ToLower()) || x.MiddleName.ToLower().Contains(searchString.ToLower()) || x.LastName.ToLower().Contains(searchString.ToLower())));
                }
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    switch (sortOrder)
                    {
                        case "firstname_asc":
                            students = students.OrderBy(x => x.FirstName).ToList();
                            break;
                        case "firstname_desc":
                            students = students.OrderByDescending(x => x.FirstName).ToList();
                            break;
                        case "middlename_asc":
                            students = students.OrderBy(x => x.MiddleName).ToList();
                            break;
                        case "middlename_desc":
                            students = students.OrderByDescending(x => x.MiddleName).ToList();
                            break;
                        case "lastname_asc":
                            students = students.OrderBy(x => x.LastName).ToList();
                            break;
                        case "lastname_desc":
                            students = students.OrderByDescending(x => x.LastName).ToList();
                            break;
                        case "estd_asc":
                            students = students.OrderBy(x => x.InsertedDate).ToList();
                            break;
                        case "estd_desc":
                            students = students.OrderByDescending(x => x.InsertedDate).ToList();
                            break;
                    }
                }
                else
                {
                    students = students.ToList();
                }
                return students.ToList();
            }
        }
    }
}
