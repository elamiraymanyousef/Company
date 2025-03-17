using AutoMapper;
using Company.DAL.Models;
using Company.PL.DTOs;

namespace Company.PL.Maping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeDTOs, Employee>();
            CreateMap<Employee, EmployeeDTOs>();

        }
    }
}
