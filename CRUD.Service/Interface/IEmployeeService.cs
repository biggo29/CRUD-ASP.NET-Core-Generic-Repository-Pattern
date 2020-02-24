using CRUD.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.Service.Interface
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByname(string name);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
