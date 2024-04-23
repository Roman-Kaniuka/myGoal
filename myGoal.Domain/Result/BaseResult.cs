namespace myGoal.Domain.Result;

public class BaseResult
{
    public bool IsSeccess => ErrorMessage==null;
    public string ErrorMessage { get; set; }

    public int? ErrorCode { get; set; }
}

public class BaseResult<T> : BaseResult
{
    public BaseResult(string errorMessage, int errorCode, T date)
    {
        ErrorMessage = errorMessage;
        ErrorCode = errorCode;
        Date = date;
    }
    public BaseResult() { }
    public T Date { get; set; }
}