namespace myGoal.Domain.Enum;

public enum ErrorCodes
{
    //Report 1-9
    ReportsNotFound = 0,
    ReportNotFound = 1,
    ReportAlreadyExists = 2,
    
    InternalServerError = 10, 
    
    //User 11-20
    UserNotFound = 11,
    UserAlreadyExists =12,
    
    //Password 21-30
    PasswordNotEqualsPasswordConfirm = 21,
    PasswordIsWrong = 22,
    
}