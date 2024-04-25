using FluentValidation;
using myGoal.Domain.Dto.Report;

namespace myGoal.Application.Validations.FluentValidations.Report;

public class CreateReportValidator : AbstractValidator<CreateReportDto>
{
    public CreateReportValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Decription).NotEmpty().MaximumLength(2000);
    }
}