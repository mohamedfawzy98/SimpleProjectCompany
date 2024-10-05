using AutoMapper;
using DAL.Models;
using Project_PL_.Models;

namespace Project_PL_.Mapping
{
    public class MappingEmployee : Profile
    {
        public MappingEmployee()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
