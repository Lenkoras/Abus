using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionDatabaseExtensions
    {
        public static IServiceCollection ConfigureSqlDatabase<TContext>(this IServiceCollection services, IConfiguration config) where TContext : DbContext
        {
            var connectionString = config.GetConnectionString();
            return services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.UseLazyLoadingProxies();
            });
        }

    }
}