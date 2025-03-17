using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Company.DAL.Models;

namespace Company.BLL.Interfaces
{
    public interface IEmployeeRepository : IGenaricRepository<Employee>
    {
        IEnumerable<Employee> GetByName(string name);
        //IEnumerable<Employee> GetAll();
        //Employee? Get(int id);
        //int Add(Employee department);
        //int Update(Employee department);
        //int Delete(Employee department);
    }
}
