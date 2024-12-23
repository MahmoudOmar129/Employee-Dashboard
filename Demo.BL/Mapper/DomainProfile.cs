using AutoMapper;
using Demo.BL.Models;
using Demo.DAL.Entity;

namespace Demo.BL.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {

            CreateMap<DepartmentVM, Department>().ReverseMap();

            CreateMap<EmployeeVM, Employee>().ReverseMap()
            .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForPath(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForPath(dest => dest.Salary, opt => opt.MapFrom(src => src.Salary))
            .ForPath(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForPath(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForPath(dest => dest.Notes, opt => opt.MapFrom(src => src.Notes))
            .ForPath(dest => dest.HireDate, opt => opt.MapFrom(src => src.HireDate))
            .ForPath(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))
            .ForPath(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
            .ForPath(dest => dest.UpdatedOn, opt => opt.MapFrom(src => src.UpdatedOn))
            .ForPath(dest => dest.DeletedOn, opt => opt.MapFrom(src => src.DeletedOn))
            .ForPath(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.IsDeleted))
            .ForPath(dest => dest.IsUpdated, opt => opt.MapFrom(src => src.IsUpdated))
            .ForPath(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
            .ForPath(dest => dest.DptId, opt => opt.MapFrom(src => src.Department.Id))
            .ForPath(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            .ForPath(dest => dest.DepartmentCode, opt => opt.MapFrom(src => src.Department.Code));



            CreateMap<CountryVM, Country>().ReverseMap();
            CreateMap<CityVM, City>().ReverseMap();
            CreateMap<DistrictVM, District>().ReverseMap();

        }


    }
}
