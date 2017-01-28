using CoreLayer;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class DepartmentService
    {
        DepartmentCRUD departmentTableActions;

        public DepartmentService()
        {
            departmentTableActions = new DepartmentCRUD();
        }

        public void SaveDepartment(Department departmentData)
        {
            departmentTableActions.SaveDepartment(departmentData);
        }

        public Department GetDepartmentById(int DepartmentID)
        {
            return departmentTableActions.GetDepartmentById(DepartmentID);
        }

        public Department GetDepartmentByName(string DepartmentName)
        {
            return departmentTableActions.GetDepartmentByName(DepartmentName);
        }
        
        public void UpdateDeparment(Department dataToBeUpdated)
        {
            departmentTableActions.UpdateDepartment(dataToBeUpdated);
        }
        public void RemoveDeparment(Department dataToBeDeleted)
        {
            departmentTableActions.RemoveDeparment(dataToBeDeleted);
        }

        public void DeleteDepartment(int DepartmentID)
        {
            Department departmentToBeDeleted = departmentTableActions.GetDepartmentById(DepartmentID);
            departmentTableActions.RemoveDeparment(departmentToBeDeleted);
        }

        public List<Department> GetAllDepartments(string sortOrder, string searchString)
        {
            return departmentTableActions.GetAllDepartments(sortOrder, searchString);
        }
    }
}
