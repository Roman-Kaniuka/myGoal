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
    UnauthorizedAccess = 13,
    UserAlreadyExistsThisRole = 14,
    
    //Password 21-30
    PasswordNotEqualsPasswordConfirm = 21,
    PasswordIsWrong = 22,
    
    //Role 31-40
    RoleAlreadyExists = 31,
    RoleNotFound = 32,
    
    
}