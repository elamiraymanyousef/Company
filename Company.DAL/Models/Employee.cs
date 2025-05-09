﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Employee: BaseEntity
    {
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public string Phone { get; set; }
        public string Salary { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime HiringDate { get; set; }
        public DateTime CreateAt { get; set; }
        //Navigation Property One to Many
        //public Department? Department { get; set; }
        // Foreign Key
        //public int? DepartmentId { get; set; }
        
        public Department? Department { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }

    }
}
