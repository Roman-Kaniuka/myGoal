using System.Xml.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using myGoal.Application.Mapping;
using myGoal.Application.Resources;
using myGoal.DAL;
using myGoal.Domain.Dto;
using myGoal.Domain.Dto.Report;
using myGoal.Domain.Entity;
using myGoal.Domain.Enum;
using myGoal.Domain.Interfaces.Repositories;
using myGoal.Domain.Interfaces.Services;
using myGoal.Domain.Interfaces.Validations;
using myGoal.Domain.Result;
using Serilog;

namespace myGoal.Application.Services;

public class ReportService : IReportService
{
    private readonly IBaseRepository<Report> _reportRepositotory;
    private readonly IBaseRepository<User> _userRepository;
    private readonly IReportValidator _reportValidator;
    private readonly IMapper _mapper;
    private readonly ILogger _logger;

    public ReportService(IBaseRepository<Report> reportRepositotory, IBaseRepository<User> userRepository, 
        IReportValidator reportValidator, ILogger logger,  IMapper mapper)
    {
        _reportRepositotory = reportRepositotory;
        _userRepository = userRepository;
        _logger = logger;
        _reportValidator = reportValidator;
        _mapper = mapper;
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

    /// <inheritdoc/>
    public async Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
    {
        ReportDto? report;
        try
        {
            report = _reportRepositotory.GetAll()
                .AsEnumerable()
                .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreateAt.ToLongDateString()))
                .FirstOrDefault(x=> x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }

        if (report == null)
        {
            _logger.Warning($"Звіт з {id} не знайдено",id);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            };
        }

        return new BaseResult<ReportDto>()
        {
            Date = report
        };
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
    {
        
        try
        {
            var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.UserId);
            var report = await _reportRepositotory.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
            var result = _reportValidator.CreateValidator(report, user);
            if (!result.IsSeccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }

            report = new Report()
            {
                Name = dto.Name,
                Description = dto.Decription,
                UserId = user.Id
            };

            await _reportRepositotory.CreateAsync(report);
            return new BaseResult<ReportDto>()
            {
                Date = _mapper.Map<ReportDto>(report)
            };

        }
        catch (Exception ex)
        {
            _logger.Error(ex, ex.Message);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    /// <inheritdoc/>
    public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
    {
        try
        {
            var report = await _reportRepositotory.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            var result =  _reportValidator.ValidateOnNull(report);
            if (!result.IsSeccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }

            var removeReport = _reportRepositotory.Remove(report);
            await _reportRepositotory.SaveChangesAsync();
            
            return new BaseResult<ReportDto>()
            {
                Date = _mapper.Map<ReportDto>(removeReport)
            };

        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message, ex);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }

    public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
    {
        try
        {
            var report = await _reportRepositotory.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
            var result = _reportValidator.ValidateOnNull(report);
            if (!result.IsSeccess)
            {
                return new BaseResult<ReportDto>()
                {
                    ErrorMessage = result.ErrorMessage,
                    ErrorCode = result.ErrorCode
                };
            }

            report.Name = dto.Name;
            report.Description = dto.Description;
            
            var updateReport = _reportRepositotory.Update(report);
            await _reportRepositotory.SaveChangesAsync();
            
            return new BaseResult<ReportDto>()
            {
                Date = _mapper.Map<ReportDto>(updateReport)
            };

        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message, ex);
            return new BaseResult<ReportDto>()
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        }
    }
}