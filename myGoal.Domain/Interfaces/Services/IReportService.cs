using myGoal.Domain.Dto;
using myGoal.Domain.Result;

namespace myGoal.Domain.Interfaces.Services;
/// <summary>
/// Сервіс відповідає за роботу з моменною частиною звіта (Report)
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Отримання всіх методів користувача
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);
}