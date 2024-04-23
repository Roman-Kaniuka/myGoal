using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using myGoal.Application.Resources;
using myGoal.DAL;
using myGoal.Domain.Dto;
using myGoal.Domain.Entity;
using myGoal.Domain.Enum;
using myGoal.Domain.Interfaces.Repositories;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Result;
using Serilog;

namespace myGoal.Application.Services;

public class ReportService : IReportService
{
    private readonly IBaseRepository<Report> _reportRepositotory;
    private readonly ILogger _logger;

    public ReportService(IBaseRepository<Report> reportRepositotory)
    {
        _reportRepositotory = reportRepositotory;
    }

    /// <inheritdoc/>
    public async Task<CollectionResult<ReportDto>> GetReportsAsync(long userId)
    {
        ReportDto[] reports;
        try
        {
            reports = await _reportRepositotory.GetAll()
                .Where(x=>x.UserId == userId)
                .Select(x=> new ReportDto(x.Id, x.Name, x.Description, x.CreateAt.ToLongDateString()))
                .ToArrayAsync();

        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError,
            };
        }

        if (!reports.Any())
        {
            _logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
            return new CollectionResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ReportsNotFound,
                ErrorCode = (int)ErrorCodes.ReportsNotFound,
            };
        }

        return new CollectionResult<ReportDto>()
        {
            Date = reports,
            Count = reports.Length
        };

    }
}