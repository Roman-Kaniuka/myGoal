using myGoal.Domain.Dto.Report;
using myGoal.Domain.Result;

namespace myGoal.Domain.Interfaces.Services;
/// <summary>
/// Сервіс відповідає за роботу з моменною частиною звіта (Report)
/// </summary>
public interface IReportService
{
    /// <summary>
    /// Отримання всіх звітів користувача
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<CollectionResult<ReportDto>> GetReportsAsync(long userId);

    /// <summary>
    /// Отримання лише одного звіту по ідентифікатору
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> GetReportByIdAsync(long id);

    /// <summary>
    /// Створення звіту з базовими параметрами
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto);

    /// <summary>
    /// Видаляє звіт по вказаному id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> DeleteReportAsync( long id);

    /// <summary>
    /// Оновлює звіт по вказаному id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto);
}