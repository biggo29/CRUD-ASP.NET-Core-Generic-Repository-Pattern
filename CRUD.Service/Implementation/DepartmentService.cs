using CRUD.Database.Models;
using CRUD.Database.UnitOfWork;
using CRUD.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRUD.Service.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private AppSettings _appSettings;

        public DepartmentService(IUnitOfWork unitOfWork, AppSettings appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
        }

        public List<Department> GetAllDepartments()
        {
            var list = _unitOfWork.Repository<Department>().GetAll().ToList();
            return list;
        }

        public Department GetDepartmentId(int id)
        {
            return _unitOfWork.Repository<Department>().GetFirstOrDefault(a => a.DeptId == id);
        }

        public Department GetDepartmentByName(string name)
        {
            return _unitOfWork.Repository<Department>().GetFirstOrDefault(a => a.DeptName.Trim() == name.Trim());
        }

        public void CreateDepartment(Department department)
        {
            _unitOfWork.Repository<Department>().Insert(department);
            _unitOfWork.Save();
        }

        public void UpdateDepartment(Department department)
        {
            _unitOfWork.Repository<Department>().Update(department);
            _unitOfWork.Save();
        }
    }
}
