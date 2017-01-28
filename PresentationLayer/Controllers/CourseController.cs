using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreLayer;
using BusinessLayer;
using PresentationLayer.Models;

namespace PresentationLayer.Controllers
{
    public class CourseController : Controller
    {
        //
        // GET: /Course/
        CourseService courseService = new CourseService();
        DepartmentService departmentService = new DepartmentService();
        LevelService levelService = new LevelService();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.DurationSortParm = sortOrder == "duration_asc" ? "duration_desc" : "duration_asc";
            ViewBag.CreditSortParm = sortOrder == "credit_asc" ? "credit_desc" : "credit_asc";
            ViewBag.EstdSortParm = sortOrder == "estd_asc" ? "estd_desc" : "estd_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);

            var Courses = courseService.GetAllCourses(sortOrder, searchString);

            return View(Courses.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult Create()
        {
            //Dropdown
            //LINQ to SQL or Object
            IList<Department> departments = departmentService.GetAllDepartments("", "");
            List<string> departmentNames = new List<string>();
            foreach (var department in departments)
            {
                departmentNames.Add(department.DepartmentName);
            }
            ViewBag.departmentNames = departmentNames;

            IList<Level> level = levelService.GetAllLevels("", "");
            List<string> levelNames = new List<string>();
            foreach (var Level in level)
            {
                levelNames.Add(Level.Semester);
            }
            ViewBag.levelNames = levelNames;

            return View();

            //List<Department> departments = departmentService.GetAllDepartments("", "");
            //List<String> departmentNames = (from department in departments select department.DepartmentName).ToList();
            //foreach (var department in departments)
            //{
            //    departmentNames.Add(department.DepartmentName);
            //}
            //ViewBag.departmentNames = departmentNames;

            //IList<Level> level = levelService.GetAllLevels("", "");
            //List<int> levelID = (from Level in level select Level.LevelID).ToList();
            //foreach (var Level in level)
            //{
            //    levelID.Add(Level.LevelID);
            //}
            //ViewBag.levelID = levelID;
            //return View();
        }

        [HttpPost]
        public ActionResult SaveCourse(CourseViewModel courseData)
        {
            Department department = departmentService.GetDepartmentByName(courseData.Department);
            Level level = levelService.GetLevelBySemester(courseData.Level);
            //courseService = new CourseService();
            Course Course = new Course()
            {
                DepartmentID = department.DepartmentID,
                LevelID = level.LevelID,
                Name = courseData.Name,
                Description = courseData.Description,
                Credit = courseData.Credit,
                Duration = courseData.Duration,
                InsertedDate = DateTime.Now,
            };
            courseService.SaveCourse(Course);
            return View(courseData);
        }


        public ActionResult UpdateCourse(int CourseId)
        {
            var course = courseService.GetCourseById(CourseId);
            return View(course);
        }

        //Course Edit
        public ActionResult Edit(int CourseID)
        {
            Course course = courseService.GetCourseById(CourseID);
            CourseViewModel courseViewModel = new CourseViewModel()
            {
                Name = course.Name,
                Duration = course.Duration,
                Credit = course.Credit,
                Description = course.Description
            };
            return View(courseViewModel);
        }

        public ActionResult DeleteCourse(int CourseId)
        {
            try
            {
                courseService.DeleteCourse(CourseId);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
