﻿using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class DepartmentRepository :GenericRepository<Department> , IDepartmentRepository
    {
        private readonly CompanyDbContext _companyDb;

        //Ask clr to create object  from CompanyDbContext 
        public DepartmentRepository(CompanyDbContext companyDb): base(companyDb)
        {
            _companyDb = companyDb;
        }

        public List<Department> GetByName(string Name)
        {

       
            return _companyDb.Departments.Where(E => E.Name.ToLower().Contains(Name.ToLower())).ToList();
        
         }
        //private readonly CompanyDbContext _context;
        //public DepartmentRepository(CompanyDbContext companyDbContext)
        //{
        //    _context = companyDbContext;
        //}
        //public IEnumerable<Department> GetAll()
        //{
        //    return _context.Departments.ToList();
        //}

        //public Department? Get(int id)
        //{
        //    return _context.Departments.Find(id);
        //}

        //public int Add(Department department)
        //{
        //    _context.Departments.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Update(Department department)
        //{
        //    _context.Departments.Update(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _context.Departments.Remove(department);
        //    return _context.SaveChanges();
        //}

    }
}
