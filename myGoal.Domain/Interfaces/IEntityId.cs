namespace myGoal.Domain.Interfaces;

public interface IEbtityId<T> where T : struct
{
    public T Id { get; set; }
}