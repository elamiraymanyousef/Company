using Company.BLL.Interfaces;
using Company.DAL.Data.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee> ,IEmployeeReposoitory
    {
        //Ask clr to create object  from CompanyDbContext 

        public EmployeeRepository(CompanyDbContext companyDb):base(companyDb)
        {
        }
        

        //private readonly CompanyDbContext _companyDb;

        //public EmployeeRepository(CompanyDbContext companyDb)
        //{
        //    _companyDb = companyDb;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return _companyDb.Employees.ToList();
        //}

        //public Employee? Get(int id)
        //{
        //    return _companyDb.Employees.Find(id);
        //}


        //public int Add(Employee employee)
        //{
        //     _companyDb.Employees.Add(employee);
        //    return _companyDb.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _companyDb.Employees.Remove(employee);
        //    return _companyDb.SaveChanges();
        //}



        //public int Update(Employee employee)
        //{
        //    _companyDb.Employees.Update(employee);
        //    return _companyDb.SaveChanges();
        //}
    }
}
