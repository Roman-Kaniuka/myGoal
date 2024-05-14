using myGoal.Domain.Interfaces;

namespace myGoal.Domain.Entity;

public class UserToken :IEbtityId<long>
{
    public long Id { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    
    
    public User User { get; set; }
    public long UserId { get; set; }
}