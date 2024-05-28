using Microsoft.EntityFrameworkCore.Storage;
using myGoal.Domain.Entity;
using myGoal.Domain.Interfaces.Repositories;
using myGoal.Domain.Interfaces.Repositories.DataBases;

namespace myGoal.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public IBaseRepository<User> Users { get; set; }
    public IBaseRepository<Role> Roles { get; set; }
    public IBaseRepository<UserRole> UserRoles { get; set; }
    private readonly ApplicationDbContext _context;
    
    public UnitOfWork(IBaseRepository<User> users, IBaseRepository<Role> roles, ApplicationDbContext context, 
        IBaseRepository<UserRole> userRoles)
    {
        _context = context;
        Users = users;
        Roles = roles;
        UserRoles = userRoles;
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await _context.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
       return await _context.SaveChangesAsync();
    }
}