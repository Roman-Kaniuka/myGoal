using myGoal.Domain.Entity;
using myGoal.Domain.Interfaces.Repositories;
using myGoal.Domain.Result;

namespace myGoal.Domain.Interfaces.Validations;

public interface IReportValidator : IBaseValidator<Report>
{
    /// <summary>
    /// Для сворення нового звіту перевіряється чи не існує звіт з такою назвою
    /// Перевіряється UserId на наявнысть, якщо немає то стоврити звіт не можливо 
    /// </summary>
    /// <param name="report"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    BaseResult CreateValidator(Report report, User user);
}