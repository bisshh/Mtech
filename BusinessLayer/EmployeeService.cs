using CoreLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class EmployeeService
    {
        EmployeeCRUD employeeTableActions;
        EmployeePersonalInfoCRUD employeePersonalInfoTableActions;

        public EmployeeService()
        {
            employeeTableActions = new EmployeeCRUD();
            employeePersonalInfoTableActions = new EmployeePersonalInfoCRUD();
        }

        public void SaveEmployee(Employee employeeData)
        {
            employeeTableActions.SaveEmployee(employeeData);
        }
                
        public Employee GetEmployeeById(int EmployeeID)
        {
            return employeeTableActions.GetEmployeeById(EmployeeID);
        }

        public EmployeePersonalInfo GetEmployeePersonalInfoById(int EmployeeID)
        {
            return employeePersonalInfoTableActions.GetEmployeePersonalInfoByID(EmployeeID);
        }

        public Employee GetEmployeeByFullName(string FirstName, string MiddleName, string LastName)
        {
            return employeeTableActions.GetEmployeeByFullName(FirstName, MiddleName, LastName);
        }

        public Employee GetEmployeeByEmail(string Email)
        {
            return employeeTableActions.GetEmployeeByEmail(Email);
        }

        public Employee GetEmployeeByName(string EmployeeName)
        {
            return employeeTableActions.GetEmployeeByName(EmployeeName);
        }
        
        public void UpdateEmployee(Employee dataToBeUpdated)
        {
            employeeTableActions.UpdateEmployee(dataToBeUpdated);
        }
        public void RemoveEmployee(Employee dataToBeDeleted)
        {
            employeeTableActions.RemoveEmployee(dataToBeDeleted);
        }
        public void RemoveEmployeePersonal(EmployeePersonalInfo dataToBeDeleted)
        {
            employeePersonalInfoTableActions.RemoveEmployee(dataToBeDeleted);
        }

        public void DeleteEmployee(int EmployeeID)
        {
            Employee employeeToBeDeleted = employeeTableActions.GetEmployeeById(EmployeeID);
            employeeTableActions.RemoveEmployee(employeeToBeDeleted);
        }

        public List<Employee> GetAllEmployees(string sortOrder, string searchString)
        {
            return employeeTableActions.GetAllEmployees(sortOrder, searchString);
        }
        public IList<EmployeePersonalInfo> GetAllEmployeePersonalInfos(string sortOrder, string searchString)
        {
            return employeePersonalInfoTableActions.GetAllEmployeesPersonalInfo(sortOrder, searchString);
        }
    }
}
