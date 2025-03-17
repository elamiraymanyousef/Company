using AutoMapper;
using Company.DAL.Models;
using Company.PL.DTOs;

namespace Company.PL.Maping
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentDTO>();

        }
    }
}
