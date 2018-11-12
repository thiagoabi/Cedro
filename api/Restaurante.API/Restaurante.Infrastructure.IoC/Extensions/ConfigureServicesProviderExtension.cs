using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using Restaurante.Infrastructure.Context;

namespace Restaurante.Infrastructure.IoC
{
    public static class ConfigurationProvider
    {
        public static IServiceCollection UseSqlServerProvider(this IServiceCollection service,  IConfigurationRoot configuration)
        {
            var connectionString = configuration.GetConnectionString("ConnectionString");
            service.AddDbContext<RestauranteContext>(options => 
            options.UseSqlServer(connectionString,
                    x => x.MigrationsAssembly(typeof(RestauranteContext).Assembly.FullName)));

            return service;
        }
    }
}
