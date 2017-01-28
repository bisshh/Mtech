using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CoreLayer;
using BusinessLayer;
using PresentationLayer.Models;
using PagedList;

namespace PresentationLayer.Controllers
{
    public class StudentController : Controller
    {
        //
        // GET: /Student/
        StudentService studentService = new StudentService();
        DepartmentService departmentService = new DepartmentService();
        LevelService levelService = new LevelService();

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = sortOrder == "fname_asc" ? "fname_dsc" : "fname_asc";
            ViewBag.MiddleNameSortParm = sortOrder == "mname_asc" ? "mname_dsc" : "mname_asc";
            ViewBag.LastNameSortParm = sortOrder == "lname_asc" ? "lname_dsc" : "lname_asc";
            ViewBag.DepartmentSortParm = sortOrder == "department_asc" ? "department_dsc" : "department_asc";
            ViewBag.LevelSortParm = sortOrder == "level_asc" ? "level_dsc" : "level_asc";
            ViewBag.EmailSortParm = sortOrder == "email_asc" ? "email_dsc" : "email_asc";
            ViewBag.AddressSortParm = sortOrder == "address_asc" ? "address_dsc" : "address_asc";
            ViewBag.AdmissionSortParm = sortOrder == "admissiondate_asc" ? "admissiondate_dsc" : "admissiondate_asc";
            ViewBag.GenderSortParm = sortOrder == "gender_asc" ? "gender_dsc" : "gender_asc";
            ViewBag.CitizenshipSortParm = sortOrder == "citizen_asc" ? "citizen_dsc" : "citizen_asc";
            ViewBag.ContactNumberSortParm = sortOrder == "contact_asc" ? "contact_dsc" : "contact_asc";
            ViewBag.GuardianNameSortParm = sortOrder == "guardianname_asc" ? "guardianname_dsc" : "guardianname_asc";
            ViewBag.GuardianContactSortParm = sortOrder == "guardiancontact_asc" ? "guardiancontact_dsc" : "guardiancontact_asc";
            ViewBag.GuardianRelationSortParm = sortOrder == "guardianrelation_asc" ? "guardianrelation_dsc" : "guardianrelation_asc";


            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.currentFilter = searchString;
            int pageSize = 3;
            int pageNumber = (page ?? 1);


            var Students = studentService.GetAllStudents(sortOrder, searchString);
            var studentPersonalInfo = studentService.GetAllStudentsPersonalInfo(sortOrder, searchString);
            var department = departmentService.GetAllDepartments(sortOrder, searchString);
            var level = levelService.GetAllLevels(sortOrder, searchString);

            var tempStudentDetails = (from student in Students
                                      join studentPersonal in studentPersonalInfo on student.StudentID equals studentPersonal.StudentID
                                      select new
                                      {
                                          studentId = student.StudentID,
                                          FirstName = student.FirstName,
                                          MiddleName = student.MiddleName,
                                          LastName = student.LastName,
                                          level = student.LevelID,
                                          DeptId = student.DepartmentID,
                                          Address = studentPersonal.Address,
                                          Contact = studentPersonal.Contact,
                                          Gender = studentPersonal.Gender,
                                          GaurdianName = studentPersonal.GuardianName,
                                          Gaurdiancontact = studentPersonal.GuardianContact,
                                          Gaurdianrelation = studentPersonal.GuardianRelation,
                                          Email = studentPersonal.Email,
                                          CitizenshipNumber = studentPersonal.CitizenshipNumber,
                                          DOB = studentPersonal.DateOfBirth,
                                          Admissiondate = studentPersonal.AdmissionDate

                                      }).ToList();


            return View(Students.ToPagedList(pageNumber, pageSize));
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
        }

        [HttpPost]
        public ActionResult SaveStudent(StudentViewModel studentData)
        {
            Department department = departmentService.GetDepartmentByName(studentData.Department);
            Level level = levelService.GetLevelBySemester(studentData.Level);

            StudentPersonalInfo StudentPersonalInfo = new StudentPersonalInfo()
            {
                StudentID = studentData.StudentID,
                Contact = studentData.Contact,
                Address = studentData.Address,
                GuardianName = studentData.GuardianName,
                GuardianContact = studentData.GuardianContact,
                GuardianRelation = studentData.GuardianRelation,
                Gender = studentData.Gender,
                Email = studentData.Email,
                CitizenshipNumber = studentData.CitizenshipNumber,
                DateOfBirth = studentData.DateOfBirth,
                AdmissionDate = studentData.AdmissionDate,
                InsertedDate = DateTime.Now
            };
                        
            Student Student = new Student()
            {
                StudentID = studentData.StudentID,
                FirstName = studentData.FirstName,
                MiddleName = studentData.MiddleName,
                LastName = studentData.LastName,
                LevelID = level.LevelID,
                DepartmentID = department.DepartmentID,
                InsertedDate = DateTime.Now,
            };

            studentService.SaveStudent(Student);
            return View(studentData);
        }
        
        public ActionResult UpdateStudent(StudentViewModel newStudentData)
        {
            Department department = departmentService.GetDepartmentByName(newStudentData.Department);
            Level level = levelService.GetLevelBySemester(newStudentData.Level);
            Student Student = new Student()
            {
                StudentID = newStudentData.StudentID,
                FirstName = newStudentData.FirstName,
                MiddleName = newStudentData.MiddleName,
                LastName = newStudentData.LastName,
                DepartmentID = department.DepartmentID,
                LevelID = level.LevelID,
                InsertedDate = DateTime.Now,
            };
            StudentPersonalInfo StudentPersonalInfo = new StudentPersonalInfo()
            {
                Address = newStudentData.Address,
                Contact = newStudentData.Contact,
                GuardianName = newStudentData.GuardianName,
                GuardianContact = newStudentData.GuardianContact,
                GuardianRelation = newStudentData.GuardianRelation,
                DateOfBirth = newStudentData.DateOfBirth,
                Email = newStudentData.Email,
                Gender = newStudentData.Gender,
                AdmissionDate = newStudentData.AdmissionDate,
                CitizenshipNumber = newStudentData.CitizenshipNumber,
                InsertedDate = DateTime.Now
            };
            studentService.UpdateStudent(Student);
            return RedirectToAction("Index");
        }
        public ActionResult DeleteStudent(string StudentID)
        {
            try
            {
                studentService.DeleteStudent(StudentID);
            }
            catch
            {
                return View();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(string StudentID)
        {
            Student student = studentService.GetStudentById(StudentID);
            StudentPersonalInfo studentPersonalInfo = studentService.GetStudentPersonalInfo(StudentID);
            StudentViewModel studentViewModel = new StudentViewModel()
            {
                StudentID = student.StudentID,
                FirstName = student.FirstName,
                MiddleName = student.MiddleName,
                LastName = student.LastName,
                //Department = student.DepartmentId,
                //Level = student.LevelId,
                Contact = studentPersonalInfo.Contact,
                Gender = studentPersonalInfo.Gender,
                Address = studentPersonalInfo.Address,
                GuardianName = studentPersonalInfo.GuardianName,
                GuardianContact = studentPersonalInfo.GuardianContact,
                GuardianRelation = studentPersonalInfo.GuardianRelation,
                Email = studentPersonalInfo.Email,
                CitizenshipNumber = studentPersonalInfo.CitizenshipNumber,
                DateOfBirth = studentPersonalInfo.DateOfBirth,
                AdmissionDate = studentPersonalInfo.AdmissionDate
            };
            return View(studentViewModel);
        }
    }
}
