

using myGoal.Domain.Interfaces;

namespace myGoal.Domain.Entity;

public class Report : IEbtityId<long>, IAuditable
{
    public long Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description{ get; set; }
    
    
    public User User { get; set; }

    public long UserId { get; set; }
    
    
    public DateTime CreateAt { get; set; }
    
    public long CreateBy { get; set; }
    
    public DateTime UpdateAt { get; set; }
    
    public long UpdateBy { get; set; }
    
}

