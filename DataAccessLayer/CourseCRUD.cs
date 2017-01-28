using CoreLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CourseCRUD
    {
        //Create Course
        public void SaveCourse(Course courses)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.course.Add(courses);
                dataContext.SaveChanges();
            }
        }

        //Read Course
        public Course GetCourseById(int CourseID)
        {
            using (var dataContext = new DataContext())
            {
                Course Course = (from course in dataContext.course
                                 where course.CourseID == CourseID
                                 select course).First();
                return Course;
            }
        }

        public Course GetCourseByName(string CourseName)
        {
            using (var dataContext = new DataContext())
            {
                Course Course = (from course in dataContext.course
                                 where course.Name == CourseName
                                 select course).First();
                return Course;
            }
        }

        //Update Course
        public void UpdateCourse(Course newCourse)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(newCourse).State = EntityState.Modified;
                dataContext.SaveChanges();
            }
        }

        //Delete Course
        public void RemoveCourse(Course courseToBeDeleted)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(courseToBeDeleted).State = EntityState.Deleted;
                dataContext.SaveChanges();
            }
        }

        public List<Course> GetAllCourses(string sortOrder, string searchString)
        {
            using (var dataContext = new DataContext())
            {
                IEnumerable<Course> courses = from Course in dataContext.course
                                              select Course;

                if (!string.IsNullOrEmpty(searchString))
                {
                    courses = courses.Where(x => (x.Name.ToLower().Contains(searchString.ToLower()) || x.Duration.ToLower().Contains(searchString.ToLower()) || x.Credit.ToLower().Contains(searchString.ToLower())));
                }
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    switch (sortOrder)
                    {
                        case "name_asc":
                            courses = courses.OrderBy(x => x.Name).ToList();
                            break;
                        case "name_desc":
                            courses = courses.OrderByDescending(x => x.Name).ToList();
                            break;
                        case "duration_asc":
                            courses = courses.OrderBy(x => x.Duration).ToList();
                            break;
                        case "duration_desc":
                            courses = courses.OrderByDescending(x => x.Duration).ToList();
                            break;
                        case "credit_asc":
                            courses = courses.OrderBy(x => x.Credit).ToList();
                            break;
                        case "credit_desc":
                            courses = courses.OrderByDescending(x => x.Credit).ToList();
                            break;
                        case "estd_asc":
                            courses = courses.OrderBy(x => x.InsertedDate).ToList();
                            break;
                        case "estd_desc":
                            courses = courses.OrderByDescending(x => x.InsertedDate).ToList();
                            break;
                    }
                }
                else
                {
                    courses = courses.ToList();
                }
                return courses.ToList();
            }
        }
    }
}
