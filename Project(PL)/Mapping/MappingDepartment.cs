using AutoMapper;
using DAL.Models;
using Project_PL_.Models;

namespace Project_PL_.Mapping
{
    public class MappingDepartment :Profile
    {
        public MappingDepartment()
        {
            CreateMap<Department, DepartmentViewModel>().ReverseMap();
        }
    }
}
