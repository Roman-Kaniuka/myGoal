using myGoal.Application.Resources;
using myGoal.Domain.Entity;
using myGoal.Domain.Enum;
using myGoal.Domain.Interfaces.Validations;
using myGoal.Domain.Result;

namespace myGoal.Application.Validations;

public class ReportValidator : IReportValidator
{
    public BaseResult ValidateOnNull(Report model)
    {
        if (model == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ReportNotFound,
                ErrorCode = (int)ErrorCodes.ReportNotFound
            };
        }

        return new BaseResult();
    }

    public BaseResult CreateValidator(Report report, User user)
    {
        if (report != null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.ReportAlreadyExists,
                ErrorCode = (int)ErrorCodes.ReportAlreadyExists
            };
        }

        if (user == null)
        {
            return new BaseResult()
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound

            };
        }

        return new BaseResult();
    }
}