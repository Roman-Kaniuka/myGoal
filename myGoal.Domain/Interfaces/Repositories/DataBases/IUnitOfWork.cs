using Microsoft.EntityFrameworkCore.Storage;
using myGoal.Domain.Entity;

namespace myGoal.Domain.Interfaces.Repositories.DataBases;

public interface IUnitOfWork : IStateSaveChanges
{
    IBaseRepository<User> Users { get; set; }
    IBaseRepository<Role> Roles { get; set; }
    IBaseRepository<UserRole> UserRoles { get; set; }
    Task<IDbContextTransaction> BeginTransactionAsync();
}