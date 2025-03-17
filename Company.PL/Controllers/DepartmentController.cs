using Company.BLL.Repositories;
using Company.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Company.PL.DTOs;
using Company.DAL.Models;
using AutoMapper;

namespace Company.PL.Controllers
{
    // MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        // Ask CLR to create an object of DepartmentRepository
        public DepartmentController(
            IDepartmentRepository departmentRepository,
             IMapper mapper
            )
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet] // GET: Department/Index
        public IActionResult Index( string? searchValue)
        {


            IEnumerable<Department> departments;
            if (string.IsNullOrEmpty(searchValue))
            {
                departments = _departmentRepository.GetAll();
            }
            else
            {
                departments = _departmentRepository.GetByName(searchValue);
            }




            //ViewData["Message"] = "Welcome To Department Page";
            ViewBag.Message = "Welcome To Department Page";


            //var departments = _departmentRepository.GetAll();
            return View(departments);
        }

        [HttpGet] // GET: Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Create(DepartmentDTO model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                #region Manual Maper
                //var department = new Department
                //{
                //    Code = model.Code,
                //    Name = model.Name,
                //    CreateAt = model.CreateAt
                //}; 
                #endregion

                // Auto Mapper
                var department = _mapper.Map<Department>(model);
                var count = _departmentRepository.Add(department);
                if (count > 0)
                { 
                    TempData["Message"] = "Department Added Successfully!";
                    return RedirectToAction("Index");

                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id , string viewName ="Details")
        {
            if (id is null)
                return BadRequest();
            var department = _departmentRepository.Get(id.Value);

            if (department is null)
                return NotFound();


            return View(viewName,department);
        }
        
        [HttpGet]
        public IActionResult Edit(int? id)
        {

            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.Get(id.Value);

            //if (department is null)
            //    return NotFound();


            return Details(id,"Edit");
        }
        // 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var count = _departmentRepository.Update(department);
                    if (count > 0)
                    {
                        TempData["Message"] = "Department Updated Successfully!";
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(department);




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
        public IActionResult Delete([FromRoute] int id, Department department)
        {

            if (ModelState.IsValid)
            {
                if (id == department.Id)
                {
                    var count = _departmentRepository.Delete(department);
                    if (count > 0)
                    {
                     TempData["Message"] = "Department Deleted Successfully!";

                       return RedirectToAction("Index");
                    }
                       
                }
            }
            return View(department);




        }

    }
}
