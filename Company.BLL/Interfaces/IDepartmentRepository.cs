﻿using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        //IEnumerable<Department> GetAll();
        //Department? Get(int id);
        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);
        List<Department> GetByName(string Name);
    }
}
