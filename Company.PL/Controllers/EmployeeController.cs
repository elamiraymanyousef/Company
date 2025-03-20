using System.Data;
using System.Reflection.Metadata;
using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.DTOs;
using Company.PL.HelperImage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Company.PL.Controllers
{
    public class EmployeeController : Controller
    {
        // ================= oldway=================
        //private readonly IEmployeeRepository _employeeRepository;

        // ================== new way =================
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IMapper mapper ,IUnitOfWork unitOfWork    /*,IEmployeeRepository employeeRepository*/  /*IDepartmentRepository departmentRepository*/)
        {
            //_employeeRepository = employeeRepository;

            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //_departmentRepository = departmentRepository;
        }



        [HttpGet]
        public IActionResult Index(string? searchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(searchValue))
            {
                 employees = _unitOfWork.employeeRepository.GetAll();
            }
            else
            {
                // for search by name
                employees = _unitOfWork.employeeRepository.GetByName(searchValue);
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
                #region Manual Maping 
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
                #endregion

                if (creatEmployeeDTO.Image is not null)
                {
                    creatEmployeeDTO.ImageName = DecumentSettings.UploadFile(creatEmployeeDTO.Image, "Images");

                }

                // =================== Using AutoMapper ===================

                var employee = _mapper.Map<Employee>(creatEmployeeDTO);
               
                _unitOfWork.employeeRepository.Add(employee);
                int count = _unitOfWork.complete();
                
                // ========================================================

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
            var employee = _unitOfWork.employeeRepository.Get(id.Value);
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
                #region Manual Maping
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

                #endregion

                if(employeeDTO.ImageName is not null && employeeDTO.ImageName is not null  )
                {
                    DecumentSettings.DeleteFile(employeeDTO.ImageName, "Images");
                }

                if(employeeDTO.Image is not null)
                {
                   employeeDTO.ImageName =  DecumentSettings.UploadFile(employeeDTO.Image, "Images");

                }
                   

                //=================== Using AutoMapper ===================
                var employee = _mapper.Map<Employee>(employeeDTO);
                employee.Id = id.Value;
                 _unitOfWork.employeeRepository.Update(employee);
                int count = _unitOfWork.complete();

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

            #region Manual Maping
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
            #endregion

            //======================== outoMapper ========================
            var employee = _mapper.Map<Employee>(employeeDTO);
            employee.Id = id.Value;
            _unitOfWork.employeeRepository.Delete(employee);
            int count = _unitOfWork.complete();
            if (count > 0)
            {
                if (employeeDTO.ImageName is not null)
                {
                    DecumentSettings.DeleteFile(employeeDTO.ImageName, "Images");

                }
                TempData["Message"] = "Employee Deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDTO);
        }
    }
}
