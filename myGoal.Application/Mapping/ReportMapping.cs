using AutoMapper;
using myGoal.Domain.Dto.Report;
using myGoal.Domain.Entity;

namespace myGoal.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>()
            .ForCtorParam(ctorParamName:"Id",m=>m.MapFrom(s =>s.Id))
            .ForCtorParam(ctorParamName:"Name",m=>m.MapFrom(s =>s.Name))
            .ForCtorParam(ctorParamName:"Description",m=>m.MapFrom(s =>s.Description))
            .ForCtorParam(ctorParamName:"DateCreated",m=>m.MapFrom(s =>s.CreateAt))
            .ReverseMap();
    }
}