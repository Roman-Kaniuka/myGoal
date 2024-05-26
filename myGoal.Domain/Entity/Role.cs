using myGoal.Domain.Interfaces;

namespace myGoal.Domain.Entity;

public class Role : IEbtityId<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<User> Users { get; set; }
}