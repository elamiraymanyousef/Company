using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Company.BLL.Repositories
{
    public class GenaricRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {
        private readonly CompanyDbContext _companyDb;

        public GenaricRepository(CompanyDbContext companyDb)
        {
            _companyDb = companyDb;
        }
        public IEnumerable<T> GetAll()
        {
            // to include department name in the employee list
            // return _companyDb.Set<T>().Include("Department").ToList();
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)_companyDb.Set<T>().Cast<Employee>().Include(e => e.Department).ToList();
            }
            return _companyDb.Set<T>().ToList();
        }

        public T? Get(int id)
        {
            return _companyDb.Set<T>().Find(id);
        }

        public int Add(T department)
        {
            _companyDb.Set<T>().Add(department);
            return _companyDb.SaveChanges();
        }

        public int Update(T department)
        {
            _companyDb.Set<T>().Update(department);
            return _companyDb.SaveChanges();
        }

        public int Delete(T department)
        {
            _companyDb.Set<T>().Remove(department);
            return _companyDb.SaveChanges();
        }
    }
    }
