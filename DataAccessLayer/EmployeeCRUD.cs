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
    public class EmployeeCRUD
    {
        //Create Employee
        public void SaveEmployee(Employee employee)
        {
            using (var dataContext = new DataContext())
            {
                var addedEmployee = dataContext.employee.Add(employee);
                employee.EmployeePersonalInfo.EmployeeID = addedEmployee.EmployeeID;
                addedEmployee.EmployeePersonalInfo = dataContext.employeePersonalInfo.Add(employee.EmployeePersonalInfo);
                dataContext.SaveChanges();
            }
        }

        //Read Employee
        public Employee GetEmployeeByFullName(string FirstName, string MiddleName, string LastName) {
            using (var dataContext = new DataContext())
            {
                try
                {
                    Employee Employee = (from employee in dataContext.employee where employee.FirstName == FirstName && employee.MiddleName == MiddleName && employee.LastName == LastName select employee).First();
                    return Employee;
                }
                catch (Exception ex) {
                    return null;
                }
            }
        } 

        public Employee GetEmployeeById(int EmployeeID)
        {
            using (var dataContext = new DataContext())
            {
                Employee employee = (from Employee in dataContext.employee
                                     where Employee.EmployeeID == EmployeeID
                                     select Employee).First();
                return employee;
            }
        }

        public Employee GetEmployeeByName(string EmployeeName)
        {
            using (var dataContext = new DataContext())
            {
                Employee employee = (from Employee in dataContext.employee
                                     where Employee.FirstName == EmployeeName
                                     select Employee).First();
                return employee;
            }
        }

        public Employee GetEmployeeByEmail(string Email)
        {
            using (var dataContext = new DataContext())
            {
                try
                {
                    Employee employee = (from Employee in dataContext.employee
                                         where Employee.Email == Email
                                         select Employee).First();
                    return employee;
                }
                catch {
                    return (null);
                }
            }
        }

        //Update Employee
        public void UpdateEmployee(Employee newEmployee)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(newEmployee).State = EntityState.Modified;
                dataContext.SaveChanges();
            }
        }

        //Delete Employee
        public void RemoveEmployee(Employee employeeToBeDeleted)
        {
            using (var dataContext = new DataContext())
            {
                dataContext.Entry(employeeToBeDeleted).State = EntityState.Deleted;
                dataContext.SaveChanges();
            }
        }

        public List<Employee> GetAllEmployees(string sortOrder, string searchString)
        {
            using (var dataContext = new DataContext())
            {
                IEnumerable<Employee> employees = from Employee in dataContext.employee
                                                      select Employee;

                if (!string.IsNullOrEmpty(searchString))
                {
                    employees = employees.Where(x => (x.FirstName.ToLower().Contains(searchString.ToLower()) || x.MiddleName.ToLower().Contains(searchString.ToLower()) || x.LastName.ToLower().Contains(searchString.ToLower())));
                }
                if (!string.IsNullOrEmpty(sortOrder))
                {
                    switch (sortOrder)
                    {
                        case "firstname_asc":
                            employees = employees.OrderBy(x => x.FirstName).ToList();
                            break;
                        case "firstname_desc":
                            employees = employees.OrderByDescending(x => x.FirstName).ToList();
                            break;
                        case "middlename_asc":
                            employees = employees.OrderBy(x => x.MiddleName).ToList();
                            break;
                        case "middlename_desc":
                            employees = employees.OrderByDescending(x => x.MiddleName).ToList();
                            break;
                        case "lastname_asc":
                            employees = employees.OrderBy(x => x.LastName).ToList();
                            break;
                        case "lastname_desc":
                            employees = employees.OrderByDescending(x => x.LastName).ToList();
                            break;
                        case "estd_asc":
                            employees = employees.OrderBy(x => x.InsertedDate).ToList();
                            break;
                        case "estd_desc":
                            employees = employees.OrderByDescending(x => x.InsertedDate).ToList();
                            break;
                    }
                }
                else
                {
                    employees = employees.ToList();
                }
                return employees.ToList();
            }
        }
    }
}
