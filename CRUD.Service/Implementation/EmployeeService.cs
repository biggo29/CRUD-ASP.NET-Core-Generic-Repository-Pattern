using CRUD.Database.Models;
using CRUD.Database.UnitOfWork;
using CRUD.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUD.Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private AppSettings _appSettings;

        public EmployeeService(IUnitOfWork unitOfWork, AppSettings appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
        }

        public List<Employee> GetAllEmployees()
        {
            var list = _unitOfWork.Repository<Employee>().GetAll().ToList();
            return list;
        }

        public Employee GetEmployeeById(int id)
        {
            return _unitOfWork.Repository<Employee>().GetFirstOrDefault(a => a.EmpId == id);
        }

        public Employee GetEmployeeByname(string name)
        {
            return _unitOfWork.Repository<Employee>().GetFirstOrDefault(a => a.EmpName.Trim() == name.Trim());
        }

        public void CreateEmployee(Employee employee)
        {
            _unitOfWork.Repository<Employee>().Insert(employee);
            _unitOfWork.Save();
        }

        public void UpdateEmployee(Employee employee)
        {
            _unitOfWork.Repository<Employee>().Update(employee);
            _unitOfWork.Save();
        }

        public void DeleteEmployee(Employee employee)
        {
            _unitOfWork.Repository<Employee>().Delete(employee);
            _unitOfWork.Save();
        }
    }
}
