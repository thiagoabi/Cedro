using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurante.Appication;
using Restaurante.Infrastructure.Context;
using Restaurante.Infrastructure.Data;
using Restaurante.Infrastructure.Repository;
using Restaurante.Infrastructure.Store;

namespace Restaurante.Infrastructure.IoC
{
    public static class BootStrapper
    {
        public static IServiceCollection RegisterData(this IServiceCollection services, IConfigurationRoot configuration)
        {
            services.UseSqlServerProvider(configuration);

            services.AddScoped<IPratoManager, PratoManager>();
            services.AddScoped<IEstabelecimentoManager, EstabelecimentoManager>();

            services.AddSingleton(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
            services.AddSingleton(typeof(IStoreBase<,>), typeof(StoreBase<,>));
            services.AddSingleton(typeof(IManagerBase<,>), typeof(ManagerBase<,>));

            services.AddScoped<IPratoRepository, PratoRepository>();            
            services.AddScoped<IPratoStore, PratoStore>();
            services.AddScoped<IEstabelecimentoRepository, EstabelecimentoRepository>();
            services.AddScoped<IEstabelecimentoStore, EstabelecimentoStore>();

            services.AddTransient<IRestauranteContext, RestauranteContext>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IApplicationBuilder ConfigureProvider(this IApplicationBuilder app, IApplicationLifetime applicationLifetime)
        {
            UnitOfWork.UpdateDatabase(app);
            return app;
        }
    }
}
