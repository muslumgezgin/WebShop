using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebShop.Infrastructure.Context;
using WebShop.Infrastructure.Repositories.Base;
using WepShop.Application.Interfaces.Context;
using WepShop.Application.Interfaces.Repository.Base;

namespace WebShop.Infrastructure
{
    public static class ServiceRegistration
    {

        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            // services.AddDbContext<ApplicationDbContext>(options => 
            //             options.UseNpgsql(configuration.GetConnectionString("DbConnection")));
            services.AddDbContext<ApplicationDBContext>(options => {
                options.UseInMemoryDatabase(databaseName: "netShopDb")
                    .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning));
                // Logging sql 
                // options.EnableSensitiveDataLogging();
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDBContext>()) ;  
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}