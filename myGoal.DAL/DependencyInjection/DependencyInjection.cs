using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using myGoal.DAL.Interceptor;

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

    }
}