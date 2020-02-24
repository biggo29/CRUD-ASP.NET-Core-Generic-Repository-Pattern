using CRUD.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Service.Interface
{
    public interface IDepartmentService
    {
        List<Department> GetAllDepartments();
        Department GetDepartmentId(int id);
        Department GetDepartmentByName(string name);
        void CreateDepartment(Department department);
        void UpdateDepartment(Department department);
    }
}
