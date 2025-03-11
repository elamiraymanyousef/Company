using System.Data;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        //Allow Clr to creat Object fron EmployeeRepository
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet] // GET: Department/Index
        public IActionResult Index()
        {
            var departments = _employeeRepository.GetAll();
            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatEmployeeDTO creatEmployeeDTO)
        {
            if (ModelState.IsValid)
            {
                var Employee = new Employee()
                {
                    Name = creatEmployeeDTO.Name,
                    Email = creatEmployeeDTO.Email,
                    Address = creatEmployeeDTO.Address,
                    Salary = creatEmployeeDTO.Salary,
                    HiringDate = creatEmployeeDTO.HiringDate,
                    IsACtive = creatEmployeeDTO.IsACtive,
                    IsDeleted = creatEmployeeDTO.IsDeleted,
                    Phone = creatEmployeeDTO.Phone,
                    Age = creatEmployeeDTO.Age
                };
                var Count = _employeeRepository.Add(Employee);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            // لما يكون فيه خطأ في الادخال يعيد البيانات الي الفورم
            return View(creatEmployeeDTO);
        }


        [HttpGet]
        public IActionResult Details(int? id,String VeiwStat= "Details")

        {
            if (id is null)
                return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null)
                return NotFound(new { StatusCode = 400, Message = $"Employee with id : {id} not found" });
            return View(VeiwStat , employee);
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int? id)
        {
            return Details(id,"Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // متنساش ال id بتاع الراوت علشان تعرف تعدل علي العنصر الصح
        public IActionResult Edit([FromRoute]int? id, CreatEmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var Employee = new Employee()
                {
                    Name = employeeDTO.Name,
                    Email = employeeDTO.Email,
                    Address = employeeDTO.Address,
                    Salary = employeeDTO.Salary,
                    HiringDate = employeeDTO.HiringDate,
                    IsACtive = employeeDTO.IsACtive,
                    IsDeleted = employeeDTO.IsDeleted,
                    Phone = employeeDTO.Phone,
                    Age = employeeDTO.Age
                };
                var Count = _employeeRepository.Update(Employee);
                if (Count > 0)
                {
                    return RedirectToAction("Index");
                }
                return View(Employee);
            }
            return View(employeeDTO);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        public IActionResult Delete([FromRoute] int? id, CreatEmployeeDTO employeeDTO)
        {
            if (!ModelState.IsValid) return BadRequest();
            var Employee = new Employee()
            {
                Id = id.Value,
                Name = employeeDTO.Name,
                Email = employeeDTO.Email,
                Address = employeeDTO.Address,
                Salary = employeeDTO.Salary,
                HiringDate = employeeDTO.HiringDate,
                IsACtive = employeeDTO.IsACtive,
                IsDeleted = employeeDTO.IsDeleted,
                Phone = employeeDTO.Phone,
                Age = employeeDTO.Age
            };
            int count = _employeeRepository.Delete(Employee);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index)); // بتحولك علي الصفحة الرئيسية
            }
            //لو مش عاوز يعمل تعديل بيبقي يرجعلك علي الفورم بتاعت الاديت
            return View(employeeDTO);
        }
    }
}
