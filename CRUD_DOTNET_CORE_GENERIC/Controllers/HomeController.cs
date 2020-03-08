using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUD_DOTNET_CORE_GENERIC.Models;
using CRUD.Database.Context;
using CRUD.Service.Interface;
using CRUD.Database.UnitOfWork;
using CRUD.Database.Models;

namespace CRUD_DOTNET_CORE_GENERIC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TESTContext _context;
        private readonly IDepartmentService _departmentService;
        private readonly IEmployeeService _employeeService;
        private readonly IUnitOfWork _unitOfWork;


        public HomeController(TESTContext context,ILogger<HomeController> logger, IDepartmentService departmentService, IEmployeeService employeeService, IUnitOfWork unitOfWork)
        {
            _context = context;
            _logger = logger;
            _departmentService = departmentService;
            _employeeService = employeeService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            //var query = _context.Employee
            //    .Join(_context.Department,
            //    e=>e.DeptId,
            //    )

            var empList = _unitOfWork.Repository<Employee>().GetAll()
                .Join(_unitOfWork.Repository<Department>().GetAll(),
                emp => emp.DeptId,
                dept => dept.DeptId,
                (emp, dept) => new { Employee = emp, Department = dept }).ToList();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
