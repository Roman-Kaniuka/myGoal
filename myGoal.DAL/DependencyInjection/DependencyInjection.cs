using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myGoal.DAL.Interceptor;
using myGoal.DAL.Repositories;
using myGoal.Domain.Entity;
using myGoal.Domain.Interfaces.Repositories;

namespace myGoal.DAL.DependencyInjection;

public static class DependencyInjection
{
    public static void AddDataAccesLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MSSQL");
        
        services.AddSingleton<DateInterceptor>();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        services.InitRepositories();
    }

    private static void InitRepositories(this IServiceCollection services)
    {
        services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
        services.AddScoped<IBaseRepository<Report>, BaseRepository<Report>>();
    }
}