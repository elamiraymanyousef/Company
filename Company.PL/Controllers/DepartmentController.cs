using Company.BLL.Interfaces;
using Company.DAL.Models;
using Company.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Company.PL.Controllers
{
    // MVC Controller
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        // Ask CLR to create an object of DepartmentRepository
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpGet] // GET: Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();
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
                var count = _departmentRepository.Add(department);
                if (count > 0)
                    return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();// stat code 400
            var department = _departmentRepository.Get(id.Value);

            if (department is null)
                return NotFound(new {StatusCode=400,Message=$"Department with id : {id} not found"});

            var departmentDTO = new DepartmentDTO
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };

            return View(departmentDTO);
        }

        [HttpGet]
        public IActionResult Edit(int? id ) 
        {
            if (id is null)
                return BadRequest();// stat code 400
            var department = _departmentRepository.Get(id.Value);

            if (department is null)
                return NotFound(new { StatusCode = 400, Message = $"Department with id : {id} not found" });

            var departmentDTO = new DepartmentDTO
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };

            return View(departmentDTO);
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
                int count = _departmentRepository.Update(departmentm);
                if (count > 0)
                {
                     return RedirectToAction(nameof(Index)); // بتحولك علي الصفحة الرئيسية
                }
            // لو مش عاوز يعمل تعديل بيبقي يرجعلك علي الفورم بتاعت الاديت
            return View(department); 
        
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();// stat code 400
            var department = _departmentRepository.Get(id.Value);
            if (department is null)
                return NotFound(new { StatusCode = 400, Message = $"Department with id : {id} not found" });
            var departmentDTO = new DepartmentDTO
            {
                Code = department.Code,
                Name = department.Name,
                CreateAt = department.CreateAt
            };
            return View(departmentDTO);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute]int? id, DepartmentDTO dTO)
        {
            if (ModelState.IsValid) return BadRequest();
            var departmentm = new Department()
            {
                Id = (int)id,
                Code = dTO.Code,
                Name = dTO.Name,
                CreateAt = dTO.CreateAt
            };
            int count = _departmentRepository.Delete(departmentm);
            if (count > 0)
            {
                return RedirectToAction(nameof(Index)); // بتحولك علي الصفحة الرئيسية
            }
            // لو مش عاوز يعمل تعديل بيبقي يرجعلك علي الفورم بتاعت الاديت
            return View(dTO);
        }

    }
}
