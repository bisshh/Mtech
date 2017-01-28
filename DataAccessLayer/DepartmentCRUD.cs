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
    public class DepartmentCRUD
    {
        //Create Department
        public void SaveDepartment(Department department)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.departments.Add(department);
                dataContext.SaveChanges();
            }
        }

        //Read Department
        public Department GetDepartmentById(int DepartmentID)
        {
            using (var dataContext = new DataContext())
            {
                Department Department = (from department in dataContext.departments
                                         where department.DepartmentID == DepartmentID
                                         select department).First();
                return Department;
            }
        }
        public Department GetDepartmentByName(string DepartmentName)
        {
            using (var dataContext = new DataContext())
            {
                Department Department = (from department in dataContext.departments
                                         where department.DepartmentName == DepartmentName
                                         select department).First();
                return Department;
            }
        }

        //Update Department
        public void UpdateDepartment(Department newDepartment)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(newDepartment).State = EntityState.Modified;
                dataContext.SaveChanges();
            }
        }

        //Delete Department
        public void RemoveDeparment(Department departmentToBeDeleted)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(departmentToBeDeleted).State = EntityState.Deleted;
                dataContext.SaveChanges();
            }
        }

        public List<Department> GetAllDepartments(string sortOrder, string searchString)
        {
            using (var dataContext = new DataContext())
            {
                IEnumerable<Department> departments = from department in dataContext.departments
                                                      select department;

                if (!string.IsNullOrEmpty(searchString))
                {
                    departments = departments.Where(x => (x.DepartmentName.ToLower().Contains(searchString.ToLower()) || x.DepartmentHead.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower())));
                }
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    switch (sortOrder)
                    {
                        case "name_asc":
                            departments = departments.OrderBy(x => x.DepartmentName).ToList();
                            break;
                        case "name_desc":
                            departments = departments.OrderByDescending(x => x.DepartmentName).ToList();
                            break;
                        case "descp_asc":
                            departments = departments.OrderBy(x => x.Description).ToList();
                            break;
                        case "descp_desc":
                            departments = departments.OrderByDescending(x => x.Description).ToList();
                            break;
                        case "depthead_asc":
                            departments = departments.OrderBy(x => x.DepartmentHead).ToList();
                            break;
                        case "depthead_desc":
                            departments = departments.OrderByDescending(x => x.DepartmentHead).ToList();
                            break;
                        case "estd_asc":
                            departments = departments.OrderBy(x => x.EstdDate).ToList();
                            break;
                        case "estd_desc":
                            departments = departments.OrderByDescending(x => x.EstdDate).ToList();
                            break;
                    }
                }
                else
                {
                    departments = departments.ToList();
                }
                return departments.ToList();
            }
        }

        //public List<Department> GetAllDepartment()
        //{
        //    using (var dataContext = new DataContext())
        //    {
        //        var result = (from department in dataContext.departments
        //                      select department).ToList();
        //        return result;
        //    }
        //}
    }
}
