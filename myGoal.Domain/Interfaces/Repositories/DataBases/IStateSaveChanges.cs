namespace myGoal.Domain.Interfaces.Repositories.DataBases;

public interface IStateSaveChanges
{
    Task<int> SaveChangesAsync();
}