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
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeReposoitory _employeeReposoitory;
        private readonly IMapper _mapper;

        // Ask CLR to create an object of DepartmentRepository
        public EmployeeController(
                IDepartmentRepository departmentRepository,
                IEmployeeReposoitory employeeReposoitory,
                IMapper mapper

                )
            {
            _departmentRepository = departmentRepository;
            _employeeReposoitory = employeeReposoitory;
            _mapper = mapper;
        }

            [HttpGet] // GET: Department/Index
            public IActionResult Index( string? searchValue)
            {

            IEnumerable<Employee> employees ;
            if (string.IsNullOrEmpty(searchValue))
            {
                employees = _employeeReposoitory.GetAll();
            }
            else
            {
                employees = _employeeReposoitory.GetByName(searchValue);
            }

            //ViewData["Message"] = "Welcome To Employee Page";
            ViewBag.Message = "Welcome To Employee Page";

             //employees = _employeeReposoitory.GetAll();
                return View(employees);
            }

        #region //[HttpGet] // GET: Department/Create
        //[HttpGet] // GET: Department/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(EmployeeDTOs model)
        //{
        //    if (ModelState.IsValid) // Server Side Validation
        //    {
        //        var employee = new Employee()
        //        {
        //            Name = model.Name,
        //            Address = model.Address,
        //            Age = model.Age,
        //            CreateAt= model.CreateAt,
        //            HiringDate = model.HiringDate,
        //            Email = model.Email,
        //            IsActive = model.IsActive,
        //            IsDeleted = model.IsDeleted,
        //            Phone = model.Phone,
        //            Salary = model.Salary
        //        };


        //    var count =_employeeReposoitory.Add(employee);
        //        if (count > 0)
        //            return RedirectToAction(nameof(Index));
        //    }
        //    return View(model);
        //} 
        #endregion

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = _departmentRepository.GetAll();
            return View();
        }
        // POST: Employee/Create
        [HttpPost]
        public IActionResult Create(EmployeeDTOs employeeDTO)
        {
            if (ModelState.IsValid)
            {
                #region Manual Maper
                //var Employee = new Employee()
                //{
                //    Name = employeeDTO.Name,
                //    Email = employeeDTO.Email,
                //    Address = employeeDTO.Address,
                //    Salary = employeeDTO.Salary,
                //    HiringDate = employeeDTO.HiringDate,
                //    IsActive = employeeDTO.IsActive,
                //    IsDeleted = employeeDTO.IsDeleted,
                //    Phone = employeeDTO.Phone,
                //    Age = employeeDTO.Age,
                //    DepartmentId = employeeDTO.DepartmentId
                //}; 
                #endregion
                var employee = _mapper.Map<Employee>(employeeDTO);
                var Count = _employeeReposoitory.Add(employee);
                if (Count > 0)
                {
                    TempData["Message"] = "Employee Added Successfully!";
                    return RedirectToAction("Index");
                }
            }
            // لما يكون فيه خطأ في الادخال يعيد البيانات الي الفورم
            return View(employeeDTO);
        }
        [HttpGet]
            public IActionResult Details(int? id, string viewName = "Details")
            {
                if (id is null)
                    return BadRequest();
                var employee = _employeeReposoitory.Get(id.Value);

                if (employee is null)
                    return NotFound();


                return View(viewName, employee);
            }

            [HttpGet]
            public IActionResult Edit(int? id)
            {

            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.Get(id.Value);

            //if (department is null)
            //    return NotFound();

            ViewData["Departments"] = _departmentRepository.GetAll();

            return Details(id, "Edit");
            }
            // 
            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit([FromRoute] int id, Employee employee)
            {

                if (ModelState.IsValid)
                {
                    if (id == employee.Id)
                    {
                        var count = _employeeReposoitory.Update(employee);
                        if (count > 0)
                    {
                        TempData["Message"] = "Employee Updated Successfully!";
                        return RedirectToAction("Index");

                    }
                }
                }
                return View(employee);




            }
             // GET: Employee/Delete/5
            [HttpGet]
            public IActionResult Delete(int? id)
            {

                //if (id is null)
                //    return BadRequest();
                //var department = _departmentRepository.Get(id.Value);

                //if (department is null)
                //    return NotFound();

                return Details(id, "Delete");
            }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Delete([FromRoute] int id, Employee employee)
            {

                if (ModelState.IsValid)
                {
                    if (id == employee.Id)
                    {
                        var count = _employeeReposoitory.Delete(employee);
                        if (count > 0)
                    {
                        TempData["Message"] = "Employee Deleted Successfully!";
                        return RedirectToAction("Index");
                    }
                    }
                }
            // لما يكون فيه خطأ في الادخال يعيد البيانات الي الفورم
            return View(employee);




            }

        }
    }

