using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;

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
                 return _companyDb.SaveChanges();        }

        public int Delete(T department)
        {

                _companyDb.Set<T>().Remove(department);
                return _companyDb.SaveChanges();        }

        }
    }
