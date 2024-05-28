using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myGoal.DAL.Interceptor;
using myGoal.DAL.Repositories;
using myGoal.Domain.Entity;
using myGoal.Domain.Interfaces.Repositories;
using myGoal.Domain.Interfaces.Repositories.DataBases;

namespace myGoal.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccesLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresSQL");
        
        services.AddSingleton<DateInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        services.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
        services.AddScoped<IBaseRepository<UserToken>, BaseRepository<UserToken>>();
        services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
        services.AddScoped<IBaseRepository<UserRole>, BaseRepository<UserRole>>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}