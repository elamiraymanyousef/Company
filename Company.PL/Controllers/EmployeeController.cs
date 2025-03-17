using System.Data;
using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository , IMapper mapper/*IDepartmentRepository departmentRepository*/)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        }



        [HttpGet]
        public IActionResult Index(string? SearchName)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchName))
            {
                 employees = _employeeRepository.GetAll();
            }
            else
            {
                // for search by name
                employees = _employeeRepository.GetByName(SearchName);
            }
            

            return View(employees);
        }



        [HttpGet]
        public IActionResult Create(/*[FromServices] IDepartmentRepository _departmentRepositor*/)
        {
            
            //ViewData["Department"] = _departmentRepository.GetAll();
            return View();
        }



        [HttpPost]
        public IActionResult Create(CreatEmployeeDTO creatEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                //var employee = new Employee()
                //{
                //    Name = creatEmployeeDTO.Name,
                //    Email = creatEmployeeDTO.Email,
                //    Address = creatEmployeeDTO.Address,
                //    Salary = creatEmployeeDTO.Salary,
                //    HiringDate = creatEmployeeDTO.HiringDate,
                //    IsACtive = creatEmployeeDTO.IsACtive,
                //    IsDeleted = creatEmployeeDTO.IsDeleted,
                //    Phone = creatEmployeeDTO.Phone,
                //    Age = creatEmployeeDTO.Age,
                //    DepartmentId = creatEmployeeDTO.DepartmentId
                //};


                // =================== Using AutoMapper ===================
                var employee = _mapper.Map<Employee>(creatEmployeeDTO);
                var count = _employeeRepository.Add(employee);

                //======== =================================================
                if (count > 0)
                {
                    TempData["Message"] = "Employee Added Successfully";
                    return RedirectToAction("Index");
                }
            }
            return View(creatEmployeeDTO);
        }

        [HttpGet]
        public IActionResult Details(int? id, string viewStat = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound(new { StatusCode = 400, Message = $"Employee with id : {id} not found" });
            return View(viewStat, employee);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute] int? id ,[FromServices] IDepartmentRepository _departmentRepository)
        {
            // TO Get All Department
            ViewData["Department"] = _departmentRepository.GetAll();
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int? id, CreatEmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                //var employee = new Employee()
                //{
                //    Name = employeeDTO.Name,
                //    Email = employeeDTO.Email,
                //    Address = employeeDTO.Address,
                //    Salary = employeeDTO.Salary,
                //    HiringDate = employeeDTO.HiringDate,
                //    IsACtive = employeeDTO.IsACtive,
                //    IsDeleted = employeeDTO.IsDeleted,
                //    Phone = employeeDTO.Phone,
                //    Age = employeeDTO.Age,
                //    DepartmentId = employeeDTO.DepartmentId
                //};

                //=================== Using AutoMapper ===================
                var employee = _mapper.Map<Employee>(employeeDTO);
                var count = _employeeRepository.Update(employee);
                if (count > 0)
                {
                    TempData["Message"] = "Employee Updated Successfully";
                    return RedirectToAction("Index");
                }
                return View(employee);
            }
            return View(employeeDTO);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }








        [HttpPost]
        public IActionResult Delete([FromRoute] int? id, CreatEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            //var employee = new Employee()
            //{
            //    Id = id.Value,
            //    Name = employeeDTO.Name,
            //    Email = employeeDTO.Email,
            //    Address = employeeDTO.Address,
            //    Salary = employeeDTO.Salary,
            //    HiringDate = employeeDTO.HiringDate,
            //    IsACtive = employeeDTO.IsACtive,
            //    IsDeleted = employeeDTO.IsDeleted,
            //    Phone = employeeDTO.Phone,
            //    Age = employeeDTO.Age
            //};

            //======================== outoMapper ========================
            var employee = _mapper.Map<Employee>(employeeDTO);
            int count = _employeeRepository.Delete(employee);
            if (count > 0)
            {
                TempData["Message"] = "Employee Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDTO);
        }
    }
}
