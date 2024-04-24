using myGoal.Domain.Result;

namespace myGoal.Domain.Interfaces.Validations;

public interface IBaseValidator <in T> where T : class
{
    BaseResult ValidateOnNull(T model);
}