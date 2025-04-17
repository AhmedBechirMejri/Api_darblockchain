using AutoMapper;
using entity.DB;
using entity.DTO;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class LeaveMappingProfile : Profile
{
    public LeaveMappingProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FullName))
            .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<LeaveRequest, LeaveRequestUpdateDTO>()
            .ForMember(dest => dest.LeaveType, opt => opt.MapFrom(src => src.LeaveType.ToString()));

        CreateMap<LeaveRequest, LeaveRequestCreationDto>().ReverseMap();

        CreateMap<LeaveRequest, LeaveRequestApproveDTO>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        CreateMap<Employee, LeaveReportDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department));

    }
}
