using AutoMapper;
using myGoal.Domain.Dto.Report;
using myGoal.Domain.Entity;

namespace myGoal.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>().ReverseMap();
    }
}