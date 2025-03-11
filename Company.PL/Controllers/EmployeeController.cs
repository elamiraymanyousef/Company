using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
      
            private readonly IEmployeeReposoitory _employeeReposoitory;

            // Ask CLR to create an object of DepartmentRepository
            public EmployeeController(IEmployeeReposoitory employeeReposoitory)
            {
                 _employeeReposoitory = employeeReposoitory;
            }

            [HttpGet] // GET: Department/Index
            public IActionResult Index()
            {
                var employees = _employeeReposoitory.GetAll();
                return View(employees);
            }

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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDTOs employeeDTO)
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
                    IsActive = employeeDTO.IsActive,
                    IsDeleted = employeeDTO.IsDeleted,
                    Phone = employeeDTO.Phone,
                    Age = employeeDTO.Age
                };
                var Count = _employeeReposoitory.Add(Employee);
                if (Count > 0)
                {
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
                            return RedirectToAction("Index");
                    }
                }
                return View(employee);




            }

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
                            return RedirectToAction("Index");
                    }
                }
                return View(employee);




            }

        }
    }

