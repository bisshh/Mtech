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
    public class DepartmentController : Controller
    {
        //
        // GET: /Department/
        DepartmentService departmentService = new DepartmentService();
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = sortOrder == "name_asc" ? "name_desc" : "name_asc";
            ViewBag.DescriptionSortParm = sortOrder == "descp_asc" ? "descp_desc" : "descp_asc";
            ViewBag.DeptHeadSortParm = sortOrder == "depthead_asc" ? "depthead_desc" : "depthead_asc";
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

            var departments = departmentService.GetAllDepartments(sortOrder, searchString);

            return View(departments.ToPagedList(pageNumber, pageSize));
        }
        
        public ActionResult Create()
        {
            return View();
        }

        //Department Save
        [HttpPost]
        public ActionResult SaveDepartment(DepartmentViewModel departmentData)
        {
            departmentService = new DepartmentService();
            Department Department = new Department()
            {
                DepartmentName = departmentData.DepartmentName,
                Description = departmentData.Description,
                DepartmentHead = departmentData.DepartmentHead,
                InsertedDate = DateTime.Now,
                EstdDate = departmentData.EstdDate
            };
            departmentService.SaveDepartment(Department);
            return View(departmentData);
        }

        //Department Update
        [HttpPost]
        public ActionResult UpdateDepartment(DepartmentViewModel newDepartmentData)
        {
            Department department = new Department()
            {
                DepartmentID = newDepartmentData.DepartmentID,
                DepartmentName = newDepartmentData.DepartmentName,
                Description = newDepartmentData.Description,
                DepartmentHead = newDepartmentData.DepartmentHead,
                EstdDate = newDepartmentData.EstdDate,
                InsertedDate = DateTime.Now,
            };
            departmentService.UpdateDeparment(department);
            return RedirectToAction("Index");
        }
        
        //Department Edit
        public ActionResult Edit(int DepartmentID)
        {
            Department department = departmentService.GetDepartmentById(DepartmentID);
            DepartmentViewModel departmentViewModel = new DepartmentViewModel()
            {
                DepartmentID = department.DepartmentID,
                DepartmentName = department.DepartmentName,
                DepartmentHead = department.DepartmentHead,
                Description = department.Description,
                EstdDate = department.EstdDate
            };
            return View(departmentViewModel);
        }

        //Department Delete
        public ActionResult DeleteDepartment(int DepartmentID)
        {
            try
            {
                departmentService.DeleteDepartment(DepartmentID);
            }
            catch {
                return View();
            }
            return RedirectToAction("Index");
        }
    }
}
