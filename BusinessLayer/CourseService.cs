using CoreLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class CourseService
    {
        CourseCRUD courseTableActions;

        public CourseService()
        {
            courseTableActions = new CourseCRUD();
        }

        public void SaveCourse(Course courseData)
        {
            courseTableActions.SaveCourse(courseData);
        }

        public Course GetCourseById(int CourseID)
        {
            return courseTableActions.GetCourseById(CourseID);
        }

        public Course GetCourseByName(string CourseName)
        {
            return courseTableActions.GetCourseByName(CourseName);
        }
        public void UpdateCourse(Course dataToBeUpdated)
        {
            courseTableActions.UpdateCourse(dataToBeUpdated);
        }
        public void RemoveCourse(Course dataToBeDeleted)
        {
            courseTableActions.RemoveCourse(dataToBeDeleted);
        }

        public void DeleteCourse(int CourseID)
        {
            Course courseToBeDeleted = courseTableActions.GetCourseById(CourseID);
            courseTableActions.RemoveCourse(courseToBeDeleted);
        }

        public IList<Course> GetAllCourses(string sortOrder, string searchString)
        {
            return courseTableActions.GetAllCourses(sortOrder, searchString);
        }
    }
}
