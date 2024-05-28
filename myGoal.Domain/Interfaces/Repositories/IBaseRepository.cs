using myGoal.Domain.Interfaces.Repositories.DataBases;

namespace myGoal.Domain.Interfaces.Repositories;

public interface IBaseRepository <TEntity> : IStateSaveChanges
{
    IQueryable<TEntity> GetAll();
    
    Task<TEntity> CreateAsync(TEntity entity);

    TEntity Update(TEntity entity);
    TEntity Remove(TEntity entity);
}