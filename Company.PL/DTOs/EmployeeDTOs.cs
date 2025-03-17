using System.ComponentModel.DataAnnotations;

namespace Company.PL.DTOs
{
    public class EmployeeDTOs
    {
        [Required(ErrorMessage ="Name Is Required !!")]
        public string Name { get; set; }
        [Range(18, 60, ErrorMessage = "Age must be between 18 and 60")]
        public int? Age { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage ="Email Invalid")]
        public string Email { get; set; }
        [RegularExpression(@"^[\w\s,.'-]{3,40}$", ErrorMessage = "Address Invalid")]


        public string Address { get; set; }
        [Phone(ErrorMessage = "Phone Invalid")]
        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public string Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HiringDate { get; set; }
        [Display(Name = "Create At")]
        public DateTime CreateAt { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
        public String? Department { get; set; }
    }
}
