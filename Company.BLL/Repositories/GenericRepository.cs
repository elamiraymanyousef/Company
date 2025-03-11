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
    public class GenericRepository<T> :IGenericRepository<T> where T :BaseEntity
    {
        private readonly CompanyDbContext _companyDb;

        public GenericRepository( CompanyDbContext companyDb)
        {
            _companyDb = companyDb;
        }
        public IEnumerable<T> GetAll()
            {
            return _companyDb.Set<T>().ToList();
        }
        public T? Get(int id)
        {
            return _companyDb.Set<T>().Find(id);
        }
      
        public int Add(T model)
        {
             _companyDb.Set<T>().Add(model);
            return _companyDb.SaveChanges();
        }
        public int Update(T model)
        {
            _companyDb.Set<T>().Update(model);
            return _companyDb.SaveChanges();
        }
        public int Delete(T model)
        {
            _companyDb.Set<T>().Remove(model);
            return _companyDb.SaveChanges();
        }
      
    }
}
