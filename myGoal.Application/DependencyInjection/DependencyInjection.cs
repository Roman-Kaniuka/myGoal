using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using myGoal.Application.Mapping;
using myGoal.Application.Services;
using myGoal.Application.Validations;
using myGoal.Application.Validations.FluentValidations;
using myGoal.Application.Validations.FluentValidations.Report;
using myGoal.Domain.Dto.Report;
using myGoal.Domain.Interfaces.Repositories;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Interfaces.Validations;

namespace myGoal.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ReportMapping));
        
        InitServices(services);
    }

    private static void InitServices(this IServiceCollection services)
    {
        services.AddScoped<IReportValidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<ITokenService, TokenService>();
    }
}