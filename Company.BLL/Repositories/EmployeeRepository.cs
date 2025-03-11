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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CompanyDbContext _companyDb;

        public EmployeeRepository(CompanyDbContext companyDb)
        {
            _companyDb = companyDb;
        }
        public IEnumerable<Employee> GetAll()
        {
            return _companyDb.Employees.ToList();
        }

        public Employee? Get(int id)
        {
            return _companyDb.Employees.Find(id);
        }

        public int Update(Employee department)
        {
            _companyDb.Employees.Update(department);
            return _companyDb.SaveChanges();
        }

        public int Add(Employee department)
        {
            _companyDb.Employees.Add(department);
            return _companyDb.SaveChanges();    
        }

        public int Delete(Employee department)
        {
            _companyDb.Employees.Remove(department);
            return _companyDb.SaveChanges();
        }

       
    }
}
