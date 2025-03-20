using AutoMapper;
using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    // MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;

        // ============== olde way==============
        //private readonly IDepartmentRepository _departmentRepository;
        // ============ new way ================
        private readonly IUnitOfWork _unitOfWork;


        // Constructor Injection
        // Ask CLR to create an object of DepartmentRepository

        public DepartmentController(IMapper mapper,IUnitOfWork unitOfWork)//IDepartmentRepository departmentRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            //_departmentRepository = departmentRepository;
        }





        [HttpGet] // GET: Department/Index
        public IActionResult Index(string? searchValue)
        {
            IEnumerable<Department> departments;
            if(string.IsNullOrEmpty(searchValue))
            {
                departments = _unitOfWork.departmentRepository.GetAll();
            }
            else
            {
                departments = _unitOfWork.departmentRepository.GetDepartmentByName(searchValue);
            }
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
                var department = new Department
                {
                    Code = model.Code,
                    Name = model.Name,
                    CreateAt = model.CreateAt
                };

                // ====== outo mapping ======
                //var department= _mapper.Map<Department>(model);
                _unitOfWork.departmentRepository.Add(department);
                int count = _unitOfWork.complete();

                if (count > 0)
                {
                    TempData["Message"] = "Department Added Successfully";
                    return RedirectToAction("Index");
                }
                   
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id ,string ViewName="Details")
        {
            if (id is null)
                return BadRequest();// stat code 400
            var department = _unitOfWork.departmentRepository.Get(id.Value);

            if (department is null)
                return NotFound(new {StatusCode=400,Message=$"Department with id : {id} not found"});

            var departmentDTO = new DepartmentDTO
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };

            return View(ViewName, departmentDTO);
        }

        [HttpGet]
        public IActionResult Edit(int? id ) 
        {
            //if (id is null)
            //    return BadRequest();// stat code 400
            //var department = _departmentRepository.Get(id.Value);

            //if (department is null)
            //    return NotFound(new { StatusCode = 400, Message = $"Department with id : {id} not found" });

            //var departmentDTO = new DepartmentDTO
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = department.CreateAt
            //};

            return Details(id, "Edit");
        }

        // ايه الي راجع من الفورم بتاعت الاديت DepartmentDTO departmentDTO
        [HttpPost]// بتاخد البيانات من الفورم وتعمل تعديل
        public IActionResult Edit([FromRoute]int id, DepartmentDTO department) 
        {
            
                if (!ModelState.IsValid) return BadRequest();
                var departmentm = new Department()
                {
                    Id=id,
                    Code = department.Code,
                    Name = department.Name,
                    CreateAt = department.CreateAt
                };
                /*int count =*/ _unitOfWork.departmentRepository.Update(departmentm);
            int count = _unitOfWork.complete();

            if (count > 0)
                {
                     TempData["Message"] = "Department Updated Successfully";
                    return RedirectToAction(nameof(Index)); // بتحولك علي الصفحة الرئيسية
                }
            // لو مش عاوز يعمل تعديل بيبقي يرجعلك علي الفورم بتاعت الاديت
            return View(department); 
        
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            //if (id is null)
            //    return BadRequest();// stat code 400
            //var department = _departmentRepository.Get(id.Value);
            //if (department is null)
            //    return NotFound(new { StatusCode = 400, Message = $"Department with id : {id} not found" });
            //var departmentDTO = new DepartmentDTO
            //{
            //    Code = department.Code,
            //    Name = department.Name,
            //    CreateAt = department.CreateAt
            //};
            return Details(id, "Delete");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int? id, DepartmentDTO dTO)
        {
           if (!ModelState.IsValid) return BadRequest();
           var departmentm = new Department()
           {
               Id = (int)id,
               Code = dTO.Code,
               Name = dTO.Name,
               CreateAt = dTO.CreateAt
           };
          /* int count =*/ _unitOfWork.departmentRepository.Delete(departmentm);
            int count = _unitOfWork.complete();

            if (count > 0)
           {
                TempData["Message"] = "Department Deleted Successfully";
                return RedirectToAction(nameof(Index)); // بتحولك علي الصفحة الرئيسية
           }
            //لو مش عاوز يعمل تعديل بيبقي يرجعلك علي الفورم بتاعت الاديت
            return View(dTO);
        }

    }
}
