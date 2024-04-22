using myGoal.Domain.Interfaces.Repositories;

namespace myGoal.DAL.Repositories;

public class BaseRepository <TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly ApplicationDbContext _dbContext;

    public BaseRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbContext.Set<TEntity>();
    }

    public Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");

        _dbContext.Add(entity);
        _dbContext.SaveChanges();
        
        return Task.FromResult(entity);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");

        _dbContext.Update(entity);
        _dbContext.SaveChanges();
        
        return Task.FromResult(entity);
    }

    public Task<TEntity> RemoveAsync(TEntity entity)
    {
        if (entity == null)
            throw new ArgumentNullException("Entity is null");

        _dbContext.Remove(entity);
        _dbContext.SaveChanges();

        return Task.FromResult(entity);
    }
}